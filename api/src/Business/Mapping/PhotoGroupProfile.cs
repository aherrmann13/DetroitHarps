namespace Business.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Models;

    public class PhotoGroupProfile : Profile
    {
        public PhotoGroupProfile()
        {
            CreateMap<IPhotoGroupModel,  PhotoGroup>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Photos,
                    opt => opt.Ignore());

            CreateMap<PhotoGroupModel,  PhotoGroup>()
                .ForMember(
                    dest => dest.Photos,
                    opt => opt.Ignore());

            CreateMap<PhotoGroup, PhotoGroupModel>();
        }
    }
} 