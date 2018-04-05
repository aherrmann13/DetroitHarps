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
        private const string TestPhotoPath = "Data/test.jpeg";
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

        private static void AssertEqual(PhotoCreateModel model, Photo entity)
        {
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
            Assert.Equal(model.Photo, entity.Data);
        }

        private PhotoCreateModel GetValidCreateModel() =>
            new PhotoCreateModel
            {
                Title = "photo",
                GroupId = _photoGroups.First().Id,
                SortOrder = 1,
                Photo = GetTestPhotoByteArray()
            };

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
    }
}