namespace DetroitHarps.Business.Test.Photo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;
    using DetroitHarps.Business.Registration;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    [Collection("AutoMapper")]
    public class PhotoManagerTest
    {
        private readonly Mock<IPhotoRepository> _photoRepositoryMock;
        private readonly Mock<IPhotoGroupRepository> _photoGroupRepositoryMock;
        private readonly Mock<ILogger<PhotoManager>> _loggerMock;

        public PhotoManagerTest()
        {
            _photoRepositoryMock = new Mock<IPhotoRepository>();
            _photoGroupRepositoryMock = new Mock<IPhotoGroupRepository>();
            _loggerMock = new Mock<ILogger<PhotoManager>>();
        }

        [Fact]
        public void NullPhotoRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoManager(null, _photoGroupRepositoryMock.Object, _loggerMock.Object));
        }

        [Fact]
        public void NullPhotoGroupRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoManager(_photoRepositoryMock.Object, null, _loggerMock.Object));
        }

        [Fact]
        public void NullloggerInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoManager(_photoRepositoryMock.Object, _photoGroupRepositoryMock.Object, null));
        }

        [Fact]
        public void CreateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<BusinessException>(() => manager.Create(null));
        }

        [Fact]
        public void CreatePhotoGroupDoesntExistThrowsTest()
        {
            var manager = GetManager();
            var model = new PhotoModel();

            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(false);

            Assert.Throws<BusinessException>(() => manager.Create(model));
        }

        [Fact]
        public void CreateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoModel();

            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            manager.Create(model);

            _photoRepositoryMock.Verify(x => x.Create(It.Is<Photo>(y => y != null)), Times.Once);
        }

        [Fact]
        public void CreateReturnsIdTest()
        {
            var id = 5;
            _photoRepositoryMock.Setup(x => x.Create(It.IsAny<Photo>())).Returns(id);
            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            var manager = GetManager();
            var model = new PhotoModel();

            var idFromManager = manager.Create(model);

            Assert.Equal(id, idFromManager);
        }

        [Fact]
        public void UpdateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<BusinessException>(() => manager.UpdateDisplayProperties(null));
        }

        [Fact]
        public void UpdatePhotoDoesntExistThrowsTest()
        {
            var manager = GetManager();
            var model = new PhotoDisplayPropertiesDetailModel();

            _photoRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(false);

            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            Assert.Throws<BusinessException>(() => manager.UpdateDisplayProperties(model));
        }

        [Fact]
        public void UpdatePhotoGroupDoesntExistThrowsTest()
        {
            var manager = GetManager();
            var model = new PhotoDisplayPropertiesDetailModel();

            _photoRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(false);

            Assert.Throws<BusinessException>(() => manager.UpdateDisplayProperties(model));
        }

        [Fact]
        public void UpdateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoDisplayPropertiesDetailModel();

            _photoRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);
            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            manager.UpdateDisplayProperties(model);

            _photoRepositoryMock.Verify(
                x => x.UpdateDisplayProperties(It.IsAny<int>(), It.Is<PhotoDisplayProperties>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void UpdateIdPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoDisplayPropertiesDetailModel { PhotoId = 2 };

            _photoRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            _photoGroupRepositoryMock
                .Setup(x => x.Exists(It.IsAny<int>()))
                .Returns(true);

            manager.UpdateDisplayProperties(model);

            _photoRepositoryMock.Verify(
                x => x.UpdateDisplayProperties(It.Is<int>(y => y == model.PhotoId), It.IsAny<PhotoDisplayProperties>()),
                Times.Once);
        }

        [Fact]
        public void DeleteIdPassedToRepositoryTest()
        {
            var manager = GetManager();
            var id = 2;

            manager.Delete(id);

            _photoRepositoryMock.Verify(
                x => x.Delete(It.Is<int>(y => y.Equals(id))),
                Times.Once);
        }

        [Fact]
        public void GetAllReturnsModelsTest()
        {
            var models = new List<Photo>() { new Photo(), new Photo() };

            _photoRepositoryMock.Setup(x => x.GetAll())
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetAll();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void GetBytesReturnsNullOnNoIdTest()
        {
            var manager = GetManager();

            var modelFromManager = manager.GetPhotoBytes(5);

            Assert.Null(modelFromManager);
        }

        [Fact]
        public void GetBytesReturnsModelTest()
        {
            var id = 4;
            var byteArray = Guid.NewGuid().ToByteArray();
            _photoRepositoryMock.Setup(x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))))
                .Returns(new Photo
                {
                    Data = new PhotoData { Data = byteArray }
                });
            var manager = GetManager();

            var modelFromManager = manager.GetPhotoBytes(id);

            Assert.NotNull(modelFromManager);
            Assert.Equal(byteArray, modelFromManager.Data);
        }

        private PhotoManager GetManager() =>
            new PhotoManager(_photoRepositoryMock.Object, _photoGroupRepositoryMock.Object, _loggerMock.Object);
    }
}