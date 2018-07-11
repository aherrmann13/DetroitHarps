namespace Business.Test.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Mapping;
    using Business.Models;
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
        public void PhotoCreateModelMapToDisplayPropertiesTest()
        {
            var photoCreateModel = new PhotoCreateModel
            {
                Title = Guid.NewGuid().ToString(),
                SortOrder = 1,
                PhotoGroupId = 2,
                MimeType = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToByteArray()
            };

            var photoDisplayProperties = Mapper.Map<PhotoDisplayProperties>(photoCreateModel);

            Assert.Equal(photoCreateModel.Title, photoDisplayProperties.Title);
            Assert.Equal(photoCreateModel.SortOrder, photoDisplayProperties.SortOrder);
            Assert.Equal(photoCreateModel.PhotoGroupId, photoDisplayProperties.PhotoGroupId);
        }

        [Fact]
        public void PhotoCreateModelMapToDataTest()
        {
            var photoCreateModel = new PhotoCreateModel
            {
                Title = Guid.NewGuid().ToString(),
                SortOrder = 1,
                PhotoGroupId = 2,
                MimeType = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToByteArray()
            };

            var photoData = Mapper.Map<PhotoData>(photoCreateModel);

            Assert.Equal(photoCreateModel.MimeType, photoData.MimeType);
            Assert.Equal(photoCreateModel.Data, photoData.Data);
        }

        [Fact]
        public void PhotoDisplayPropertiesModelMapTest()
        {
            var photoDisplayPropertiesModel = new PhotoDisplayPropertiesModel
            {
                Id = 3,
                Title = Guid.NewGuid().ToString(),
                SortOrder = 1,
                PhotoGroupId = 2
            };

            var photoDisplayProperties = Mapper.Map<PhotoDisplayProperties>(photoDisplayPropertiesModel);

            Assert.Equal(photoDisplayPropertiesModel.Id, photoDisplayProperties.Id);
            Assert.Equal(photoDisplayPropertiesModel.Title, photoDisplayProperties.Title);
            Assert.Equal(photoDisplayPropertiesModel.SortOrder, photoDisplayProperties.SortOrder);
            Assert.Equal(photoDisplayPropertiesModel.PhotoGroupId, photoDisplayProperties.PhotoGroupId);
        }

        [Fact]
        public void PhotoDisplayPropertiesMapTest()
        {
            var photoDisplayProperties = new PhotoDisplayProperties
            {
                Id = 3,
                Title = Guid.NewGuid().ToString(),
                SortOrder = 1,
                PhotoGroupId = 2
            };

            var photoDisplayPropertiesModel = Mapper.Map<PhotoDisplayPropertiesModel>(photoDisplayProperties);

            Assert.Equal(photoDisplayProperties.Id, photoDisplayPropertiesModel.Id);
            Assert.Equal(photoDisplayProperties.Title, photoDisplayPropertiesModel.Title);
            Assert.Equal(photoDisplayProperties.SortOrder, photoDisplayPropertiesModel.SortOrder);
            Assert.Equal(photoDisplayProperties.PhotoGroupId, photoDisplayPropertiesModel.PhotoGroupId);
        }

        [Fact]
        public void PhotoDataMapTest()
        {
            var photoData = new PhotoData
            {
                Id = 3,
                MimeType = Guid.NewGuid().ToString(),
                Data = Guid.NewGuid().ToByteArray()
            };

            var photoDataModel = Mapper.Map<PhotoDataModel>(photoData);

            Assert.Equal(photoData.MimeType, photoDataModel.MimeType);
            Assert.Equal(photoData.Data, photoDataModel.Data);
        }
    } 
}