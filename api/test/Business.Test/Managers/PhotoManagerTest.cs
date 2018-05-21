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
            model.GroupId = GetNonExistantId<PhotoGroup>();

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
            var model = GetValidUpdateModel();

            var response = _manager.Update(model);

            var entity = DbContext.Set<Photo>()
                .AsNoTracking()
                .First(x => x.Id.Equals(model.Id));

            AssertEqual(model, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void UpdateNullModelExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Update(null));
        }

        [Fact]
        public void UpdateIdDoesntExistExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Update(null));
        }

        [Fact]
        public void UpdateNonExistantGroupIdExceptionTest()
        {
            SeedPhotos();
            var model = GetValidUpdateModel();

            model.GroupId = GetNonExistantId<PhotoGroup>();

            Assert.Throws<InvalidOperationException>(() => _manager.Update(model));
        }

        [Fact]
        public void UpdateNonExistantIdExceptionTest()
        {
            SeedPhotos();
            var model = GetValidUpdateModel();

            model.Id = GetNonExistantId<Photo>();

            Assert.Throws<InvalidOperationException>(() => _manager.Update(model));
        }

        [Fact]
        public void DeleteSuccessTest()
        {
            var seededEntities = SeedPhotos().ToList();

            var id = seededEntities.First().Id;

            var response = _manager.Delete(id);

            var entities = DbContext.Set<Photo>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(seededEntities.Count - 1, entities.Count);
            Assert.Null(entities.FirstOrDefault(x => x.Id.Equals(id)));
            Assert.Equal(response, id);
        }

        [Fact]
        public void DeleteNonExistantIdExceptionTest()
        {
            SeedPhotos();

            var id = GetNonExistantId<Photo>();

            Assert.Throws<InvalidOperationException>(() => _manager.Delete(id));
        }

        [Fact]
        public void GetMetadataTest()
        {
            SeedPhotos();

            var response = _manager.GetMetadata().ToList();

            var entities = DbContext.Set<Photo>()
                .AsNoTracking()
                .ToList();

            AssertEqual(response, entities);
        }

        [Fact]
        public void GetMetadataByIdTest()
        {
            var entities = SeedPhotos().ToList();

            var id = entities.First().Id;

            var response = _manager.GetMetadata(id);

            var entity = DbContext.Set<Photo>()
                .AsNoTracking()
                .First(x => x.Id.Equals(id));

            AssertEqual(response, entity);
        }

        [Fact]
        public void GetMetadataNonExistantIdExceptionTest()
        {
            SeedPhotos();

            var id = GetNonExistantId<Photo>();

            Assert.Throws<InvalidOperationException>(() => _manager.GetMetadata(id));
        }
        
        [Fact]
        public void GetSuccessTest()
        {
            var entities = SeedPhotos().ToList();
            var id = entities.First().Id;

            var response = _manager.Get(id);

            var entity = DbContext.Set<Photo>()
                .AsNoTracking()
                .First(x => x.Id.Equals(id));

            AssertEqual(response, entity);
        }

        [Fact]
        public void GetDoesNotExistExceptionTest()
        {
            var entities = SeedPhotos().ToList();
            var id = entities.Select(x => x.Id).Max() + 1;

            Assert.Throws<InvalidOperationException>(() => _manager.Get(id));
        }

        private static void AssertEqual(PhotoCreateModel model, Photo entity)
        {
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
            Assert.Equal(model.Data, entity.Data);
        }

        private static void AssertEqual(PhotoMetadataUpdateModel model, Photo entity)
        {
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
        }

        private static void AssertEqual(PhotoMetadataReadModel model, Photo entity)
        {
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
        }

        private static void AssertEqual(PhotoReadModel model, Photo entity)
        {
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
            Assert.Equal(model.Data, entity.Data);
        }
        
        private static void AssertEqual(IList<PhotoMetadataReadModel> models, IList<Photo> entities)
        {
            Assert.Equal(models.Count, entities.Count);
            foreach(var model in models)
            {
                var entity = entities.First(x => x.Id.Equals(model.Id));

                AssertEqual(model, entity);
            }
        }

        private PhotoCreateModel GetValidCreateModel() =>
            new PhotoCreateModel
            {
                Title = "photo",
                GroupId = _photoGroups.First().Id,
                SortOrder = 1,
                Data = GetTestPhotoByteArray()
            };

        private PhotoMetadataUpdateModel GetValidUpdateModel()
        {
            var photo = DbContext.Set<Photo>().AsNoTracking().First();

            return new PhotoMetadataUpdateModel
            {
                Id = photo.Id,
                Title = Guid.NewGuid().ToString(),
                SortOrder = 10,
                GroupId = _photoGroups
                    .First(x => !x.Id.Equals(photo.PhotoGroupId))
                    .Id
            };
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