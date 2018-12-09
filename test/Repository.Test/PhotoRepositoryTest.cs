namespace DetroitHarps.Repository.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Moq;
    using Tools;
    using Xunit;

    public class PhotoRepositoryTest
    {
        private readonly Mock<DetroitHarpsDbContext> _dbContextMock;
        private readonly PhotoRepository _repository;

        public PhotoRepositoryTest()
        {
            _dbContextMock = new Mock<DetroitHarpsDbContext>(
                new DbContextOptions<DetroitHarpsDbContext>());
            _repository = new PhotoRepository(_dbContextMock.Object);
        }

        [Fact]
        public void UpdatePhotoDisplayPropertiesThrowsOnNullTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => _repository.UpdateDisplayProperties(default(int), null));
        }

        [Fact]
        public void UpdatePhotoDisplayPropertiesSkipsIfDoesntExistTest()
        {
            _dbContextMock
                .Setup(x => x.Set<Photo>())
                .Returns(new List<Photo>().AsQueryableMockDbSet());

            _repository.UpdateDisplayProperties(default(int), new PhotoDisplayProperties());

            _dbContextMock.Verify(x => x.SaveChanges(), Times.Never);
        }

        [Fact]
        public void UpdatePhotoDisplayPropertiesDoesntUpdatePhotoDataTest()
        {
            // TODO: clean this up
            var entity = new Photo
            {
                DisplayProperties = new PhotoDisplayProperties
                {
                    PhotoGroupId = 4,
                    Title = Guid.NewGuid().ToString(),
                    SortOrder = 5
                },
                Data = new PhotoData
                {
                    MimeType = Guid.NewGuid().ToString(),
                    Data = Guid.NewGuid().ToByteArray(),
                }
            };

            var updatedProperties = new PhotoDisplayProperties
            {
                PhotoGroupId = 2,
                Title = Guid.NewGuid().ToString(),
                SortOrder = 3
            };

            var dbContext = InMemoryDbContextProvider.GetContext();
            var respository = new PhotoRepository(dbContext);

            dbContext.Add(entity);
            dbContext.SaveChanges();
            DetachAllEntities(dbContext);

            respository.UpdateDisplayProperties(entity.Id, updatedProperties);

            var photos = dbContext.Set<Photo>().AsNoTracking().ToList();
            Assert.Single(photos);
            Assert.Equal(entity.Data.Data, photos[0].Data.Data);
            Assert.Equal(entity.Data.MimeType, photos[0].Data.MimeType);
            Assert.Equal(
                updatedProperties.PhotoGroupId,
                photos[0].DisplayProperties.PhotoGroupId);
            Assert.Equal(
                updatedProperties.SortOrder,
                photos[0].DisplayProperties.SortOrder);
            Assert.Equal(
                updatedProperties.Title,
                photos[0].DisplayProperties.Title);
        }

        [Fact]
        public void PhotoExistsWithGroupIdTest()
        {
            var entity = new Photo
            {
                DisplayProperties = new PhotoDisplayProperties
                {
                    PhotoGroupId = 2
                }
            };
            _dbContextMock.Setup(x => x.Set<Photo>())
                .Returns(new List<Photo> { entity }.AsQueryableMockDbSet());

            _dbContextMock.Setup(x => x.Set<PhotoDisplayProperties>())
                .Returns(new List<PhotoDisplayProperties>
                    {
                        entity.DisplayProperties
                    }.AsQueryableMockDbSet());

            var existingGroupIdResult =
                _repository.PhotosExistWithGroupId(entity.DisplayProperties.PhotoGroupId);

            var nonExistingGroupIdResult =
                _repository.PhotosExistWithGroupId(entity.DisplayProperties.PhotoGroupId + 1);

            Assert.True(existingGroupIdResult);
            Assert.False(nonExistingGroupIdResult);
        }

        private static void DetachAllEntities(DbContext dbContext)
        {
            dbContext.ChangeTracker.Entries()
                .ToList()
                .ForEach(x => x.State = EntityState.Detached);
        }
    }
}