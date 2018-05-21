namespace Business.Mapping
{
    using System.Linq;
    using AutoMapper;
    using Business.Models;
    using Repository.Entities;

    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoModelBase,  Photo>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Data,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PhotoGroup,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PhotoGroupId,
                    opt => opt.MapFrom(src => src.GroupId));

            CreateMap<PhotoCreateModel, Photo>()
                .IncludeBase<PhotoModelBase,  Photo>()
                .ForMember(
                    dest => dest.Data,
                    opt => opt.MapFrom(src => src.Data));

            CreateMap<PhotoMetadataUpdateModel, Photo>()
                .IncludeBase<PhotoModelBase,  Photo>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<Photo, PhotoMetadataReadModel>()
                .ForMember(
                    dest => dest.GroupId,
                    opt => opt.MapFrom(src => src.PhotoGroupId));

            CreateMap<Photo, PhotoReadModel>()
                .ForMember(
                    dest => dest.GroupId,
                    opt => opt.MapFrom(src => src.PhotoGroupId));
        }
    }
}