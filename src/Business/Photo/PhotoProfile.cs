namespace DetroitHarps.Business.Photo
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;

    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoModel, Photo>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<PhotoDisplayPropertiesModel, PhotoDisplayProperties>()
                .ReverseMap();

            CreateMap<PhotoDataModel, PhotoData>()
                .ReverseMap();

            CreateMap<Photo, PhotoDisplayPropertiesDetailModel>()
                .ForPath(
                    dest => dest.PhotoId,
                    opt => opt.MapFrom(src => src.Id))
                .ForPath(
                    dest => dest.PhotoGroupId,
                    opt => opt.MapFrom(src => src.DisplayProperties.PhotoGroupId))
                .ForPath(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.DisplayProperties.Title))
                .ForPath(
                    dest => dest.SortOrder,
                    opt => opt.MapFrom(src => src.DisplayProperties.SortOrder))
                .ReverseMap();
        }
    }
}