namespace DetroitHarps.Business.Photo
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;

    public class PhotoGroupProfile : Profile
    {
        public PhotoGroupProfile()
        {
            CreateMap<PhotoGroupCreateModel, PhotoGroup>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<PhotoGroupModel, PhotoGroup>();

            CreateMap<PhotoGroup, PhotoGroupModel>();
        }
    }
}