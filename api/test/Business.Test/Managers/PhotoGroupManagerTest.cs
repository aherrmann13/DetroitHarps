namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Business.Entities;
    using Business.Abstractions;
    using Business.Managers;
    using Business.Models;
    using Moq;
    using Xunit;

    [Collection("AutoMapper")]
    public class PhotoGroupManagerTest
    {
        private readonly Mock<IPhotoGroupRepository> _photoGroupRepositoryMock;
        
        public PhotoGroupManagerTest()
        {
            _photoGroupRepositoryMock = new Mock<IPhotoGroupRepository>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoGroupManager(null));
        }

        [Fact]
        public void CreateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.Create(null));  
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
            var id = 5;
            _photoGroupRepositoryMock.Setup(x => x.Create(It.IsAny<PhotoGroup>()))
                .Returns(id);
            var manager = GetManager();
            var model = new PhotoGroupCreateModel();

            var idFromManager = manager.Create(model);

            Assert.Equal(id, idFromManager);
        }

        [Fact]
        public void UpdateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.Update(null));  
        }

        [Fact]
        public void UpdateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoGroupModel();

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
        public void GetAllReturnsModelsTest()
        {
            var models = new List<PhotoGroup>()
            {
                new PhotoGroup(),
                new PhotoGroup()
            };
            _photoGroupRepositoryMock.Setup(x => x.GetAll())
                .Returns(models);

            var manager = GetManager();

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
            var id = 4;
            _photoGroupRepositoryMock.Setup(
                    x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))))
                .Returns(new PhotoGroup());
            var manager = GetManager();

            var modelFromManager = manager.Get(id);

            Assert.NotNull(modelFromManager);
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoGroupManager(null));
        }

        private PhotoGroupManager GetManager() =>
            new PhotoGroupManager(_photoGroupRepositoryMock.Object);
    } 
}