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

    public class PhotoProfileTest
    {
        public PhotoProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<PhotoProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void PhotoMapTest()
        {
            var model = new PhotoModel
            {
                Data = new PhotoDataModel(),
                DisplayProperties = new PhotoDisplayPropertiesModel()
            };

            var entity = Mapper.Map<Photo>(model);

            Assert.Equal(default(int), entity.Id);
            Assert.NotNull(entity.Data);
            Assert.NotNull(entity.DisplayProperties);
        }

        [Fact]
        public void PhotoDisplayPropertiesMapTest()
        {
            var model = new PhotoDisplayPropertiesModel
            {
                Title = Guid.NewGuid().ToString(),
                PhotoGroupId = 2,
                SortOrder = 3
            };

            var entity = Mapper.Map<PhotoDisplayProperties>(model);

            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.PhotoGroupId, entity.PhotoGroupId);
            Assert.Equal(model.SortOrder, entity.SortOrder);
        }

        [Fact]
        public void PhotoDisplayPropertiesModelMapTest()
        {
            var entity = new PhotoDisplayProperties
            {
                Title = Guid.NewGuid().ToString(),
                PhotoGroupId = 2,
                SortOrder = 3
            };

            var model = Mapper.Map<PhotoDisplayPropertiesModel>(entity);

            Assert.Equal(entity.Title, model.Title);
            Assert.Equal(entity.PhotoGroupId, model.PhotoGroupId);
            Assert.Equal(entity.SortOrder, model.SortOrder);
        }

        [Fact]
        public void PhotoDataMapTest()
        {
            var model = new PhotoDataModel
            {
                MimeType = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToByteArray()
            };

            var entity = Mapper.Map<PhotoData>(model);

            Assert.Equal(model.MimeType, entity.MimeType);
            Assert.Equal(model.Data, entity.Data);
        }

        [Fact]
        public void PhotoDataModelMapTest()
        {
            var entity = new PhotoData
            {
                MimeType = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToByteArray()
            };

            var model = Mapper.Map<PhotoDataModel>(entity);

            Assert.Equal(entity.MimeType, model.MimeType);
            Assert.Equal(entity.Data, model.Data);
        }

        [Fact]
        public void PhotoDisplayPropertiesDetailModelMapToPhotoTest()
        {
            var model = new PhotoDisplayPropertiesDetailModel
            {
                PhotoId = 1,
                Title = Guid.NewGuid().ToString(),
                PhotoGroupId = 2,
                SortOrder = 3
            };

            var entity = Mapper.Map<Photo>(model);

            Assert.Null(entity.Data);
            Assert.Equal(model.PhotoId, entity.Id);
            Assert.Equal(model.Title, entity.DisplayProperties.Title);
            Assert.Equal(model.PhotoGroupId, entity.DisplayProperties.PhotoGroupId);
            Assert.Equal(model.SortOrder, entity.DisplayProperties.SortOrder);
        }

        [Fact]
        public void PhotoMapToPhotoDisplayPropertiesDetailModelTest()
        {
            var entity = new Photo
            {
                Id = 1,
                DisplayProperties = new PhotoDisplayProperties
                {
                    Title = Guid.NewGuid().ToString(),
                    PhotoGroupId = 2,
                    SortOrder = 3
                },
                Data = new PhotoData()
            };

            var model = Mapper.Map<PhotoDisplayPropertiesDetailModel>(entity);

            Assert.Equal(entity.Id, model.PhotoId);
            Assert.Equal(entity.DisplayProperties.Title, model.Title);
            Assert.Equal(entity.DisplayProperties.PhotoGroupId, model.PhotoGroupId);
            Assert.Equal(entity.DisplayProperties.SortOrder, model.SortOrder);
        }
    }
}