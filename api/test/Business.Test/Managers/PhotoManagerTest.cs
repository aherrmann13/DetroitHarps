namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    using Repository.Entities;
    using Tools;
    using Xunit;

    public class PhotoManagerTest : ManagerTestBase
    {
        private const string TestPhotoPath = "Data/test.jpg";
        private readonly IPhotoManager _manager;
        private readonly IList<PhotoGroup> _photoGroups;

        public PhotoManagerTest() : base()
        {
            _manager = ServiceProvider.GetRequiredService<IPhotoManager>();
            _photoGroups = SeedPhotoGroups().ToList();
        }

        [Fact]
        public void CreateSuccessTest()
        {
            var model = GetValidCreateModel();

            var response = _manager.Create(model);

            var entity = DbContext.Set<Photo>()
                .AsNoTracking()
                .First();

            AssertEqual(model, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void CreateNullModelExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Create(null));

            var entity = DbContext.Set<Photo>()
                .AsNoTracking()
                .FirstOrDefault();

            Assert.Null(entity);
        }

        [Fact]
        public void CreateNonExistantGroupIdExceptionTest()
        {
            var model = GetValidCreateModel();

            model.GroupId = _photoGroups.Select(x => x.Id).Max() + 1;

            Assert.Throws<InvalidOperationException>(() => _manager.Create(model));

            var entity = DbContext.Set<Photo>()
                .AsNoTracking()
                .FirstOrDefault();

            Assert.Null(entity);
        }

        [Fact]
        public void UpdateSuccessTest()
        {
            SeedPhotos();
            var models = GetValidUpdateModels().ToList();

            var response = _manager.Update(models.ToArray()).ToList();

            var updatedEntities = DbContext.Set<Photo>()
                .AsNoTracking()
                .ToList();

            AssertEqual(models, updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        [Fact]
        public void UpdateSuccessNotAllIdsExistTest()
        {
            SeedPhotos();
            var models = GetValidUpdateModels().ToList();

            var nonExistantId = models.Select(x => x.Id).Max() + 1;
            models[0].Id = nonExistantId;

            var response = _manager.Update(models.ToArray()).ToList();

            var entities = DbContext.Set<Photo>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(models.Count, entities.Count);

            var updatedEntities = entities.Where(x => models.Select(y => y.Id).Contains(x.Id)).ToList();

            AssertEqual(models.Where(x => !x.Id.Equals(nonExistantId)).ToList(), updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        [Fact]
        public void UpdateNullModelExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Update(null));
        }

        [Fact]
        public void UpdateSuccessNullInModelTest()
        {
            SeedPhotos();
            var models = GetValidUpdateModels().ToList();

            var nonExistantId = models.Select(x => x.Id).Max() + 1;
            models[0] = null;

            var modelIds = models.Where(x => x != null).Select(x => x.Id).ToList();

            var response = _manager.Update(models.ToArray()).ToList();

            var entities = DbContext.Set<Photo>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(models.Count, entities.Count);

            var updatedEntities = entities.Where(x => modelIds.Contains(x.Id)).ToList();

            AssertEqual(models.Where(x => x != null).ToList(), updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        [Fact]
        public void UpdateNonExistantGroupIdExceptionTest()
        {
            SeedPhotos();
            var models = GetValidUpdateModels().ToList();

            models[0].GroupId = _photoGroups.Select(x => x.Id).Max() + 1;

            Assert.Throws<InvalidOperationException>(() => _manager.Update(models.ToArray()));
        }

        private static void AssertEqual(PhotoCreateModel model, Photo entity)
        {
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
            Assert.Equal(model.Photo, entity.Data);
        }

        private static void AssertEqual(PhotoMetadataUpdateModel model, Photo entity)
        {
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
        }

        private static void AssertEqual(IList<PhotoMetadataUpdateModel> models, IList<Photo> entities)
        {
            Assert.Equal(models.Count, entities.Count);
            foreach(var model in models)
            {
                var entity = entities.First(x => x.Id.Equals(model.Id));

                AssertEqual(model, entity);
            }
        }

        private static void AssertResponseCorrect(IList<int> response, IList<Photo> entities)
        {
            Assert.Equal(response.Count, entities.Count);

            Assert.Equal(response.OrderBy(x => x), entities.Select(x => x.Id).OrderBy(x => x));
        }

        private PhotoCreateModel GetValidCreateModel() =>
            new PhotoCreateModel
            {
                Title = "photo",
                GroupId = _photoGroups.First().Id,
                SortOrder = 1,
                Photo = GetTestPhotoByteArray()
            };

        private IEnumerable<PhotoMetadataUpdateModel> GetValidUpdateModels()
        {
            var photos = DbContext.Set<Photo>().AsNoTracking().ToList();

            for(var i = 0; i < photos.Count; i++)
            {
                yield return new PhotoMetadataUpdateModel
                {
                    Id = photos[i].Id,
                    Title = Guid.NewGuid().ToString(),
                    SortOrder = i + 10,
                    GroupId = _photoGroups
                        .Where(x => !x.Id.Equals(photos[i].PhotoGroupId))
                        .First()
                        .Id
                };
            }
        }

        private byte[] GetTestPhotoByteArray() =>
            System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), TestPhotoPath));
        private IEnumerable<PhotoGroup> SeedPhotoGroups()
        {
            var list = new List<PhotoGroup>();
            for(var i = 0; i < 5; i++)
            {
                list.Add(new PhotoGroup
                {
                    Name = $"Group{i}",
                    SortOrder = i
                });
            }

            DbContext.AddRange(list);
            DbContext.SaveChanges();

            return list;
        }

        private IEnumerable<Photo> SeedPhotos()
        {
            var list = new List<Photo>();

            var groupIds = _photoGroups.Take(2).ToList();

            for(var i = 0; i < 5; i++)
            {
                list.Add(new Photo
                {
                    PhotoGroupId = groupIds[i < 2 ? 0 : 1].Id,
                    Title = $"Title{i}",
                    SortOrder = i,
                    Data = GetTestPhotoByteArray()
                });
            }

            DbContext.AddRange(list);
            DbContext.SaveChanges();

            return list;
        }
    }
}