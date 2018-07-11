namespace Business.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Models;

    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<IPhotoDisplayPropertiesModel,  PhotoDisplayProperties>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PhotoData,
                    opt => opt.Ignore());

            CreateMap<IPhotoDataModel, PhotoData>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<PhotoDisplayPropertiesModel, PhotoDisplayProperties>()
                .ForMember(
                    dest => dest.PhotoData,
                    opt => opt.Ignore());;

            CreateMap<PhotoDisplayProperties, PhotoDisplayPropertiesModel>();

            CreateMap<PhotoData, PhotoDataModel>();
        }
    }
} 