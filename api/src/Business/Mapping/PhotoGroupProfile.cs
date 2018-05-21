namespace Business.Mapping
{
    using System.Linq;
    using AutoMapper;
    using Business.Models;
    using Repository.Entities;

    public class PhotoGroupProfile : Profile
    {
        public PhotoGroupProfile()
        {
            CreateMap<PhotoGroupModelBase,  PhotoGroup>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Photos,
                    opt => opt.Ignore());

            CreateMap<PhotoGroupCreateModel, PhotoGroup>()
                .IncludeBase<PhotoGroupModelBase,  PhotoGroup>();

            // TODO : better way of mapping this
            CreateMap<PhotoGroupUpdateModel, PhotoGroup>()
                .IncludeBase<PhotoGroupModelBase,  PhotoGroup>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<PhotoGroup, PhotoGroupReadModel>()
                .ForMember(
                    dest => dest.PhotoIds,
                    opt => opt.MapFrom(src => src.Photos.Select(y => y.Id)));
        }
    }
}