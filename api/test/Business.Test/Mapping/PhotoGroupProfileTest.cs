namespace Business.Test.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Mapping;
    using Business.Models;
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
            var photoGroupModel = new PhotoGroupCreateModel
            {
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var photoGroup = Mapper.Map<PhotoGroup>(photoGroupModel);

            Assert.Equal(photoGroupModel.Name, photoGroup.Name);
            Assert.Equal(photoGroupModel.SortOrder, photoGroup.SortOrder);
        }

        [Fact]
        public void PhotoGroupModelMapTest()
        {
            var photoGroupModel = new PhotoGroupModel
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var photoGroup = Mapper.Map<PhotoGroup>(photoGroupModel);

            Assert.Equal(photoGroupModel.Id, photoGroup.Id);
            Assert.Equal(photoGroupModel.Name, photoGroup.Name);
            Assert.Equal(photoGroupModel.SortOrder, photoGroup.SortOrder);
        }

        [Fact]
        public void PhotoGroupMapTest()
        {
            var photoGroup = new PhotoGroup
            {
                Id = 1,
                Name = Guid.NewGuid().ToString(),
                SortOrder = 1
            };

            var photoGroupModel = Mapper.Map<PhotoGroupModel>(photoGroup);

            Assert.Equal(photoGroup.Id, photoGroupModel.Id);
            Assert.Equal(photoGroup.Name, photoGroupModel.Name);
            Assert.Equal(photoGroup.SortOrder, photoGroupModel.SortOrder);
        }
    } 
}