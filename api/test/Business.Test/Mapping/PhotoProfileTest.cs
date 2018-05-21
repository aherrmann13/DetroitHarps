namespace Business.Test
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Mapping;
    using Business.Models;
    using Repository.Entities;
    using Xunit;

    public class PhotoProfileTest
    {
        public PhotoProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<PhotoProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void PhotoModelCreateTest()
        {
            var model = new PhotoCreateModel
            {
                Title = Guid.NewGuid().ToString(),
                GroupId = 2,
                SortOrder = 1,
                Data = Guid.NewGuid().ToByteArray()
            };

            var entity = Mapper.Map<Photo>(model);

            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
            Assert.Equal(model.SortOrder, entity.SortOrder);
            Assert.Equal(model.Data, entity.Data);
        }

        [Fact]
        public void PhotoModelUpdateMetadataTest()
        {
            var model = new PhotoMetadataUpdateModel
            {
                Id = 1,
                Title = Guid.NewGuid().ToString(),
                GroupId = 2,
                SortOrder = 1,
            };

            var entity = Mapper.Map<Photo>(model);

            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.GroupId, entity.PhotoGroupId);
            Assert.Equal(model.SortOrder, entity.SortOrder);
        }

        [Fact]
        public void PhotoModelReadMetadataTest()
        {
            var entity = new Photo
            {
                Id = 2,
                Title = Guid.NewGuid().ToString(),
                PhotoGroupId = 2,
                SortOrder = 1,
                Data = Guid.NewGuid().ToByteArray()
            };

            var model = Mapper.Map<PhotoMetadataReadModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Title, model.Title);
            Assert.Equal(entity.PhotoGroupId, model.GroupId);
            Assert.Equal(entity.SortOrder, model.SortOrder);
        }

        [Fact]
        public void PhotoModelReadTest()
        {
            var entity = new Photo
            {
                Id = 2,
                Title = Guid.NewGuid().ToString(),
                PhotoGroupId = 2,
                SortOrder = 1,
                Data = Guid.NewGuid().ToByteArray()
            };

            var model = Mapper.Map<PhotoReadModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Title, model.Title);
            Assert.Equal(entity.PhotoGroupId, model.GroupId);
            Assert.Equal(entity.SortOrder, model.SortOrder);
            Assert.Equal(entity.Data, model.Data);
        }
    }
}