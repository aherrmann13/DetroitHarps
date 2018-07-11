namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Business.Entities;
    using Business.Abstractions;
    using Business.Managers;
    using Business.Models;
    using Moq;
    using Xunit;

    [Collection("AutoMapper")]
    public class PhotoManagerTest
    {
        private readonly Mock<IPhotoRepository> _photoRepositoryMock;
        
        public PhotoManagerTest()
        {
            _photoRepositoryMock = new Mock<IPhotoRepository>();
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTestTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoManager(null));
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
            var model = new PhotoCreateModel
            {
                Data = Guid.NewGuid().ToByteArray()
            };

            manager.Create(model);

            _photoRepositoryMock.Verify(
                x => x.Create(It.Is<PhotoDisplayProperties>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void CreateModelPhotoDataPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoCreateModel
            {
                Data = Guid.NewGuid().ToByteArray()
            };

            manager.Create(model);

            _photoRepositoryMock.Verify(
                x => x.Create(It.Is<PhotoDisplayProperties>(y => y.PhotoData != null)),
                Times.Once);
        }

        [Fact]
        public void CreateReturnsIdTest()
        {
            var id = 5;
            _photoRepositoryMock.Setup(x => x.Create(It.IsAny<PhotoDisplayProperties>()))
                .Returns(id);
            var manager = GetManager();
            var model = new PhotoCreateModel();

            var idFromManager = manager.Create(model);

            Assert.Equal(id, idFromManager);
        }

        [Fact]
        public void UpdateNullModelThrowsTest()
        {
            var manager = GetManager();

            Assert.Throws<ArgumentNullException>(() => manager.UpdateDisplayProperties(null));  
        }

        [Fact]
        public void UpdateModelPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoDisplayPropertiesModel();

            manager.UpdateDisplayProperties(model);

            _photoRepositoryMock.Verify(
                x => x.Update(It.Is<PhotoDisplayProperties>(y => y != null)),
                Times.Once);
        }

        [Fact]
        public void UpdateModelPhotoDataNotPassedToRepositoryTest()
        {
            var manager = GetManager();
            var model = new PhotoDisplayPropertiesModel();

            manager.UpdateDisplayProperties(model);

            _photoRepositoryMock.Verify(
                x => x.Update(It.Is<PhotoDisplayProperties>(y => y.PhotoData == null)),
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
            var models = new List<PhotoDisplayProperties>()
            {
                new PhotoDisplayProperties(),
                new PhotoDisplayProperties()
            };
            _photoRepositoryMock.Setup(x => x.GetAll())
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetAll();

            Assert.Equal(models.Count, modelsFromManager.Count());
            Assert.All(modelsFromManager, x => Assert.NotNull(x));
        }

        [Fact]
        public void GetByGroupIdReturnsModelsTest()
        {
            var models = new List<PhotoDisplayProperties>()
            {
                new PhotoDisplayProperties(),
                new PhotoDisplayProperties()
            };
            _photoRepositoryMock.Setup(
                    x => x.GetMany(It.IsAny<Expression<Func<PhotoDisplayProperties, bool>>>()))
                .Returns(models);

            var manager = GetManager();

            var modelsFromManager = manager.GetByGroupId(2);

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
            _photoRepositoryMock.Setup(
                    x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))))
                .Returns(new PhotoDisplayProperties());
            var manager = GetManager();

            var modelFromManager = manager.Get(id);

            Assert.NotNull(modelFromManager);
        }

        [Fact]
        public void GetBytesReturnsNullOnNoIdTest()
        {
            var manager = GetManager();

            var modelFromManager = manager.Get(5);

            Assert.Null(modelFromManager);
        }

        [Fact]
        public void GetBytesReturnsModelTest()
        {
            var id = 4;
            var byteArray = Guid.NewGuid().ToByteArray();
            _photoRepositoryMock.Setup(
                    x => x.GetSingleOrDefault(It.Is<int>(y => y.Equals(id))))
                .Returns(new PhotoDisplayProperties
                {
                    PhotoData = new PhotoData
                    {
                        Data = byteArray
                    }
                });
            var manager = GetManager();

            var modelFromManager = manager.GetPhotoBytes(id);

            Assert.NotNull(modelFromManager);
            Assert.Equal(byteArray, modelFromManager.Data);
        }

        [Fact]
        public void NullRepositoryInConstructorThrowsTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoManager(null));
        }

        private PhotoManager GetManager() =>
            new PhotoManager(_photoRepositoryMock.Object);
    } 
}