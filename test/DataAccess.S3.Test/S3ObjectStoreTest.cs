namespace DetroitHarps.DataAccess.S3.Test
{
    using System;
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
        private readonly Mock<IAmazonS3> _s3Client;
        private readonly S3ObjectStoreSettings _settings;
        private readonly S3ObjectStore<TestObject, int> _store;

        public S3ObjectStoreTest()
        {
            _s3Client = new Mock<IAmazonS3>();
            _settings = new S3ObjectStoreSettings
            {
                BucketName = "mybucket",
                KeyPrefix = "keyprefix"
            };
            _store = new S3ObjectStore<TestObject, int>(_s3Client.Object, _settings);
        }

        [Fact]
        public void NullIAmazonS3InConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new S3ObjectStore<TestObject, int>(null, _settings));
        }

        [Fact]
        public void NullS3ObjectStoreSettingsInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new S3ObjectStore<TestObject, int>(_s3Client.Object, null));
        }

        [Fact]
        public void PutCallsPutInS3WithBucketNameFromSettingsTest()
        {
            _store.Put(new TestObject { Id = 1, PropOne = "some property", PropTwo = 40 }).Wait();

            _s3Client.Verify(
                x => x.PutObjectAsync(
                    It.Is<PutObjectRequest>(y => y.BucketName == "mybucket"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void PutCallsPutInS3WithKeyPrefixAndIdAsKeyTest()
        {
            _store.Put(new TestObject { Id = 1, PropOne = "some property", PropTwo = 40 }).Wait();

            _s3Client.Verify(
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
            _s3Client.Verify(
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
            _s3Client
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
            _s3Client
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
        public void GetThrowsDataAccessExceptionIfGetCallThrowsTest()
        {
            var ex = new Exception();
            _s3Client
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

            _s3Client.Verify(
                x => x.DeleteObjectAsync(
                    It.Is<DeleteObjectRequest>(y => y.BucketName == "mybucket"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void DeleteCallsDeleteInS3WithKeyPrefixAndIdAsKeyTest()
        {
            _store.Delete(1).Wait();

            _s3Client.Verify(
                x => x.DeleteObjectAsync(
                    It.Is<DeleteObjectRequest>(y => y.Key == "keyprefix/1"), It.IsAny<CancellationToken>()),
                    Times.Once);
        }

        [Fact]
        public void DeleteThrowsDataAccessExceptionIfDeleteCallThrowsTest()
        {
            var ex = new Exception();
            _s3Client
                .Setup(x => x.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>()))
                .Throws(ex);
            var thrown = Assert.ThrowsAsync<DataAccessException>(() => _store.Delete(1)).Result;
            Assert.Equal("Error Deleting File", thrown.Message);
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