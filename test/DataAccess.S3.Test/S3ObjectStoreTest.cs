namespace DetroitHarps.DataAccess.S3.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using Amazon.S3;
    using Amazon.S3.Model;
    using DetroitHarps.Business;
    using DetroitHarps.DataAccess.S3;
    using Moq;
    using Xunit;

    public class S3ObjectStoreTest
    {
        private readonly Mock<IAmazonS3> _mockS3Client;
        private readonly S3ObjectStoreSettings _settings;
        private readonly Mock<IKeyConverter<int>> _mockKeyConverter;
        private readonly S3ObjectStore<TestObject, int> _store;

        public S3ObjectStoreTest()
        {
            _mockS3Client = new Mock<IAmazonS3>();
            _settings = new S3ObjectStoreSettings
            {
                BucketName = "mybucket",
                KeyPrefix = "keyprefix"
            };
            _mockKeyConverter = new Mock<IKeyConverter<int>>();
            _store = new S3ObjectStore<TestObject, int>(_mockS3Client.Object, _settings, _mockKeyConverter.Object);

            _mockKeyConverter.Setup(x => x.ToString(It.IsAny<int>())).Returns<int>(x => x.ToString());
            _mockKeyConverter.Setup(x => x.FromString(It.IsAny<string>())).Returns<string>(x => int.Parse(x));
        }

        [Fact]
        public void NullIAmazonS3InConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new S3ObjectStore<TestObject, int>(
                    null,
                    _settings,
                    _mockKeyConverter.Object));
        }

        [Fact]
        public void NullS3ObjectStoreSettingsInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new S3ObjectStore<TestObject, int>(
                    _mockS3Client.Object,
                    null,
                    _mockKeyConverter.Object));
        }

        [Fact]
        public void NullIKeyConverterInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => new S3ObjectStore<TestObject, int>(
                    _mockS3Client.Object,
                    _settings,
                    null));
        }

        [Fact]
        public void PutCallsPutInS3WithBucketNameFromSettingsTest()
        {
            _store.Put(new TestObject { Id = 1, PropOne = "some property", PropTwo = 40 }).Wait();

            _mockS3Client.Verify(
                x => x.PutObjectAsync(
                    It.Is<PutObjectRequest>(y => y.BucketName == "mybucket"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void PutCallsPutInS3WithKeyPrefixAndIdAsKeyTest()
        {
            _store.Put(new TestObject { Id = 1, PropOne = "some property", PropTwo = 40 }).Wait();

            _mockS3Client.Verify(
                x => x.PutObjectAsync(
                    It.Is<PutObjectRequest>(y => y.Key == "keyprefix/1"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void PutCallsPutInS3WithSerializedObjectTest()
        {
            _store.Put(new TestObject { Id = 1, PropOne = "some property", PropTwo = 40 }).Wait();

            var expectedContent = "{\"Id\":1,\"PropOne\":\"some property\",\"PropTwo\":40}";

            // TODO: how does idisposable work in expression
            _mockS3Client.Verify(
                x => x.PutObjectAsync(
                    It.Is<PutObjectRequest>(
                        y => new StreamReader(y.InputStream, Encoding.UTF8).ReadToEnd() == expectedContent),
                        It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void PutThrowsDataAccessExceptionIfPutCallThrowsTest()
        {
            var ex = new Exception();
            _mockS3Client
                .Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>()))
                .Throws(ex);
            var thrown = Assert.ThrowsAsync<DataAccessException>(
                () => _store.Put(new TestObject { Id = 1, PropOne = "some property", PropTwo = 40 }))
                .Result;
            Assert.Equal("Error Putting File", thrown.Message);
            Assert.Equal(ex, thrown.InnerException);
        }

        [Fact]
        public void GetReturnsDeserializedObjectFromS3Test()
        {
            var obj = Encoding.UTF8.GetBytes("{\"Id\":1,\"PropOne\":\"some property\",\"PropTwo\":40}");
            var response = new GetObjectResponse
            {
                ResponseStream = new MemoryStream(obj)
            };
            _mockS3Client
                .Setup(
                    x => x.GetObjectAsync(
                        It.Is<GetObjectRequest>(
                            y => y.BucketName == "mybucket" && y.Key == "keyprefix/1"),
                            It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = _store.Get(1).Result;

            Assert.Equal(1, result.Id);
            Assert.Equal("some property", result.PropOne);
            Assert.Equal(40, result.PropTwo);
        }

        [Fact]
        public void GetReturnsObjectDefaultWhenKeyDoesntExistFromS3Test()
        {
            var ex = new AmazonS3Exception("test ex");
            ex.StatusCode = System.Net.HttpStatusCode.NotFound;
            _mockS3Client
                .Setup(
                    x => x.GetObjectAsync(
                        It.Is<GetObjectRequest>(
                            y => y.BucketName == "mybucket" && y.Key == "keyprefix/1"),
                            It.IsAny<CancellationToken>()))
                .ThrowsAsync(ex);

            var result = _store.Get(1).Result;

            Assert.Equal(default(TestObject), result);
        }

        [Fact]
        public void GetThrowsDataAccessExceptionIfGetCallThrowsTest()
        {
            var ex = new Exception();
            _mockS3Client
                .Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .Throws(ex);
            var thrown = Assert.ThrowsAsync<DataAccessException>(() => _store.Get(1)).Result;
            Assert.Equal("Error Getting File", thrown.Message);
            Assert.Equal(ex, thrown.InnerException);
        }

        [Fact]
        public void GetThrowsDataAccessExceptionIfGetCallThrowsAmazonS3ExceptionTest()
        {
            var ex = new AmazonS3Exception("test");
            _mockS3Client
                .Setup(x => x.GetObjectAsync(It.IsAny<GetObjectRequest>(), It.IsAny<CancellationToken>()))
                .Throws(ex);
            var thrown = Assert.ThrowsAsync<DataAccessException>(() => _store.Get(1)).Result;
            Assert.Equal("Error Getting File", thrown.Message);
            Assert.Equal(ex, thrown.InnerException);
        }

        [Fact]
        public void DeleteCallsDeleteInS3WithBucketNameFromSettingsTest()
        {
            _store.Delete(1).Wait();

            _mockS3Client.Verify(
                x => x.DeleteObjectAsync(
                    It.Is<DeleteObjectRequest>(y => y.BucketName == "mybucket"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void DeleteCallsDeleteInS3WithKeyPrefixAndIdAsKeyTest()
        {
            _store.Delete(1).Wait();

            _mockS3Client.Verify(
                x => x.DeleteObjectAsync(
                    It.Is<DeleteObjectRequest>(y => y.Key == "keyprefix/1"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void DeleteThrowsDataAccessExceptionIfDeleteCallThrowsTest()
        {
            var ex = new Exception();
            _mockS3Client
                .Setup(x => x.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
                .Throws(ex);
            var thrown = Assert.ThrowsAsync<DataAccessException>(() => _store.Delete(1)).Result;
            Assert.Equal("Error Deleting File", thrown.Message);
            Assert.Equal(ex, thrown.InnerException);
        }

        [Fact]
        public void GetAllIdsReturnsAllIdsFromListObjectsV2Test()
        {
            var response = new ListObjectsV2Response
            {
                S3Objects = new List<S3Object>()
                {
                    new S3Object { Key = "keyprefix/1" },
                    new S3Object { Key = "keyprefix/2" },
                    new S3Object { Key = "keyprefix/3" }
                }
            };
            _mockS3Client
                .Setup(
                    x => x.ListObjectsV2Async(
                        It.Is<ListObjectsV2Request>(
                            y => y.BucketName == "mybucket" && y.Prefix == "keyprefix/" && y.Delimiter == "/"),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            var result = _store.GetAllIds().Result;

            Assert.Equal(3, result.Count);
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);
        }

        [Fact]
        public void GetAllIdsThrowsDataAccessExceptionIfListObjectsV2CallThrowsTest()
        {
            var ex = new Exception();
            _mockS3Client
                .Setup(
                    x => x.ListObjectsV2Async(
                        It.Is<ListObjectsV2Request>(
                            y => y.BucketName == "mybucket" && y.Prefix == "keyprefix/" && y.Delimiter == "/"),
                        It.IsAny<CancellationToken>()))
                .ThrowsAsync(ex);
            var thrown = Assert.ThrowsAsync<DataAccessException>(() => _store.GetAllIds()).Result;
            Assert.Equal("Error Listing Objects", thrown.Message);
            Assert.Equal(ex, thrown.InnerException);
        }

        private class TestObject : IHasId<int>
        {
            public int Id { get; set; }

            public string PropOne { get; set; }

            public int PropTwo { get; set; }
        }
    }
}