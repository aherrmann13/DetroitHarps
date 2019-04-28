namespace DetroitHarps.Business.Test.Photo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;
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
        public void PhotoGroupCreateModelMapTest()
        {
            var model = new PhotoGroupCreateModel
            {
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var entity = Mapper.Map<PhotoGroup>(model);

            Assert.Equal(default(int), entity.Id);
            Assert.Equal(entity.Name, entity.Name);
            Assert.Equal(entity.SortOrder, entity.SortOrder);
        }

        [Fact]
        public void PhotoGroupModelMapTest()
        {
            var model = new PhotoGroupModel
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
        public void PhotoGroupMapTest()
        {
            var entity = new PhotoGroup
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var model = Mapper.Map<PhotoGroupModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.SortOrder, model.SortOrder);
        }
    }
}