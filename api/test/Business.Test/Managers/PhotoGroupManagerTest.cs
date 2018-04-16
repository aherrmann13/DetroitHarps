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

    public class PhotoGroupManagerTest : ManagerTestBase
    {
        private const string TestPhotoPath = "Data/test.jpg";

        private readonly IPhotoGroupManager _manager;

        public PhotoGroupManagerTest() : base()
        {
            _manager = ServiceProvider.GetRequiredService<IPhotoGroupManager>();
        }

        [Fact]
        public void CreateSuccessTest()
        {
            var model = GetValidCreateModel();

            var response = _manager.Create(model);

            var entity = DbContext.Set<PhotoGroup>()
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
        public void UpdateSuccessTest()
        {
            SeedPhotoGroups();

            var model = GetValidUpdateModel();

            var response = _manager.Update(model);

            var entity = DbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .First(x => x.Id.Equals(model.Id));

            AssertEqual(model, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void UpdateNullModelExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Create(null));
        }

        [Fact]
        public void UpdateIdDoesntExistExceptionTest()
        {
            var entities = SeedPhotoGroups();
            var model = GetValidUpdateModel();
            model.Id = entities.Select(x => x.Id).Max() + 1;
            
            Assert.Throws<InvalidOperationException>(() => _manager.Update(model));
        }

        [Fact]
        public void DeleteSuccessTest()
        {
            var entities = SeedPhotoGroups().ToList();

            var id = entities.First().Id;

            var relatedEntities = entities.First().Photos;
            DbContext.RemoveRange(relatedEntities);
            DbContext.SaveChanges();

            var response = _manager.Delete(id);

            var entity = DbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Id.Equals(id));

            Assert.Null(entity);
            Assert.Equal(response, id);
        }

        [Fact]
        public void DeleteWithPhotosExcepttionTest()
        {
            var entities = SeedPhotoGroups().ToList();

            var id = entities.First().Id;

            Assert.Throws<InvalidOperationException>(() => _manager.Delete(id));
        }

        [Fact]
        public void DeleteIdDoesntExistExceptionTest()
        {
            var entities = SeedPhotoGroups();
            
            var id = entities.Select(x => x.Id).Max() + 1;
            
            Assert.Throws<InvalidOperationException>(() => _manager.Delete(id));
        }

        [Fact]
        public void GetAllTest()
        {
            SeedPhotoGroups();

            var response = _manager.GetAll().ToList();

            var entities  = DbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .Include(x => x.Photos)
                .ToList();

            AssertEqual(response, entities);
        }

        [Fact]
        public void GetTest()
        {
            var entities = SeedPhotoGroups();

            var id = entities.First().Id;

            var response = _manager.Get(id);

            var entity  = DbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .Include(x => x.Photos)
                .First(x => x.Id.Equals(id));

            AssertEqual(response, entity);
        }

        [Fact]
        public void GetIdDoesntExistTest()
        {
            var entities = SeedPhotoGroups();

            var id = entities.Select(x => x.Id).Max() + 1;

            Assert.Throws<InvalidOperationException>(() => _manager.Get(id));
        }

        private static void AssertEqual(PhotoGroupCreateModel model, PhotoGroup entity)
        {
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.SortOrder, entity.SortOrder);
        }

        private static void AssertEqual(PhotoGroupUpdateModel model, PhotoGroup entity)
        {
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.SortOrder, entity.SortOrder);
        }

        private static void AssertEqual(PhotoGroupReadModel model, PhotoGroup entity)
        {
            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(
                model.PhotoIds.OrderBy(x => x),
                entity.Photos.Select(x => x.Id).OrderBy(x => x));
        }

        private static void AssertEqual(
            IList<PhotoGroupReadModel> models,
            IList<PhotoGroup> entities)
        {
            Assert.Equal(models.Count, entities.Count);
            foreach(var model in models)
            {
                var entity = entities.FirstOrDefault(x => x.Id.Equals(model.Id));

                Assert.NotNull(entity);
            }

        }

        private byte[] GetTestPhotoByteArray() =>
            System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), TestPhotoPath));

        private PhotoGroupCreateModel GetValidCreateModel() =>
            new PhotoGroupCreateModel
            {
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

        private PhotoGroupUpdateModel GetValidUpdateModel() =>
            new PhotoGroupUpdateModel
            {
                Id = DbContext.Set<PhotoGroup>().Select(x => x.Id).First(),
                Name = Guid.NewGuid().ToString(),
                SortOrder = 10
            };

        private IEnumerable<PhotoGroup> SeedPhotoGroups()
        {
            var list = new List<PhotoGroup>();
            for(var i = 0; i < 5; i++)
            {
                list.Add(new PhotoGroup
                {
                    Name = $"Group{i}",
                    SortOrder = i,
                    Photos = 
                    {
                        new Photo
                        {
                            Title = $"Group{i}-Photo1",
                            SortOrder = 1
                        },
                        new Photo
                        {
                            Title = $"Group{i}-Photo2",
                            SortOrder = 2
                        }
                    }
                });
            }

            DbContext.AddRange(list);
            DbContext.SaveChanges();

            return list;
        }
    }
}