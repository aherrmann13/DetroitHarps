namespace DetroitHarps.Business.Test.Photo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    [Collection("AutoMapper")]
    public class PhotoGroupManagerTest
    {
        private readonly Mock<IPhotoGroupRepository> _photoGroupRepositoryMock;
        private readonly Mock<IPhotoRepository> _photoRepositoryMock;
        private readonly Mock<ILogger<PhotoGroupManager>> _loggerMock;

        public PhotoGroupManagerTest()
        {
            _photoGroupRepositoryMock = new Mock<IPhotoGroupRepository>();
            _photoRepositoryMock = new Mock<IPhotoRepository>();
            _loggerMock = new Mock<ILogger<PhotoGroupManager>>();
        }

        [Fact]
        public void NullPhotoGroupRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PhotoGroupManager(null, _photoRepositoryMock.Object, _loggerMock.Object));
        }

        [Fact]
        public void NullPhotoRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PhotoGroupManager(_photoGroupRepositoryMock.Object, null, _loggerMock.Object));
        }

        [Fact]
        public void NullLoggerInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PhotoGroupManager(_photoGroupRepositoryMock.Object, _photoRepositoryMock.Object, null));
        }

        [Fact]
        public void CreateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<BusinessException>(() => manager.Create(null));
        }

        [Fact]
        public void CreateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoGroupCreateModel();

            manager.Create(model);

            _photoGroupRepositoryMock.Verify(
                x => x.Create(It.Is<PhotoGroup>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void CreateReturnsIdTest()
        {
            var manager = GetManager();
            var model = new PhotoGroupCreateModel();

            var id = 5;
            _photoGroupRepositoryMock.Setup(x => x.Create(It.IsAny<PhotoGroup>())).Returns(id);

            var idFromManager = manager.Create(model);

            Assert.Equal(id, idFromManager);
        }

        [Fact]
        public void UpdateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<BusinessException>(() => manager.Update(null));
        }

        [Fact]
        public void UpdateIdDoesntExistThrowsTest()
        {
            var manager = GetManager();
            var model = new PhotoGroupModel();

            _photoGroupRepositoryMock.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);

            Assert.Throws<BusinessException>(() => manager.Update(model));
        }

        [Fact]
        public void UpdateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoGroupModel();

            _photoGroupRepositoryMock.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);

            manager.Update(model);

            _photoGroupRepositoryMock.Verify(
                x => x.Update(It.Is<PhotoGroup>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void DeleteIdPassedToRepositoryTest()
        {
            var manager = GetManager();
            var id = 2;

            manager.Delete(id);

            _photoGroupRepositoryMock.Verify(
                x => x.Delete(It.Is<int>(y => y.Equals(id))),
                Times.Once);
        }

        [Fact]
        public void DeleteThrowsWhenGroupContainsPhotosTest()
        {
            var manager = GetManager();
            var id = 2;
            _photoRepositoryMock.Setup(x => x.PhotosExistWithGroupId(It.IsAny<int>())).Returns(true);

            Assert.Throws<BusinessException>(() => manager.Delete(id));

            _photoRepositoryMock.Verify(
                x => x.PhotosExistWithGroupId(It.Is<int>(y => y.Equals(id))),
                Times.Once);
        }

        [Fact]
        public void GetAllReturnsModelsTest()
        {
            var manager = GetManager();
            var models = new List<PhotoGroup>() { new PhotoGroup(), new PhotoGroup() };

            _photoGroupRepositoryMock.Setup(x => x.GetAll()).Returns(models);

            var modelsFromManager = manager.GetAll();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void GetReturnsNullOnNoIdTest()
        {
            var manager = GetManager();

            var modelFromManager = manager.Get(5);

            Assert.Null(modelFromManager);
        }

        [Fact]
        public void GetReturnsModelTest()
        {
            var manager = GetManager();

            var id = 4;
            _photoGroupRepositoryMock
                .Setup(x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))))
                .Returns(new PhotoGroup());

            var modelFromManager = manager.Get(id);

            Assert.NotNull(modelFromManager);
        }

        private PhotoGroupManager GetManager() =>
            new PhotoGroupManager(
                _photoGroupRepositoryMock.Object,
                _photoRepositoryMock.Object,
                _loggerMock.Object);
    }
}