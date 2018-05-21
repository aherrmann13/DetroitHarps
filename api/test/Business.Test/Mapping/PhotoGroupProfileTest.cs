namespace Business.Test
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Mapping;
    using Business.Models;
    using Repository.Entities;
    using Xunit;

    public class PhotoGroupProfileTest
    {
        public PhotoGroupProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<PhotoGroupProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void PhotoGroupModelCreateTest()
        {
            var model = new PhotoGroupCreateModel
            {
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var entity = Mapper.Map<PhotoGroup>(model);

            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.SortOrder, entity.SortOrder);
        }

        [Fact]
        public void PhotoGroupModelUpdateTest()
        {
            var model = new PhotoGroupUpdateModel
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var entity = Mapper.Map<PhotoGroup>(model);

            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Name, entity.Name);
            Assert.Equal(model.SortOrder, entity.SortOrder);
        }

        [Fact]
        public void PhotoGroupModelReadTest()
        {
            var entity = new PhotoGroup
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1,
                Photos = { new Photo { Id = 2 } }
            };

            var model = Mapper.Map<PhotoGroupReadModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.SortOrder, model.SortOrder);
            Assert.Equal(entity.Photos.Select(x => x.Id), model.PhotoIds);
        }
    }
}