namespace Business.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Models;
    using Repository.Entities;

    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<ChildInformationModelBase,  RegisteredChild>()
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.DateTime))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.RegisteredPerson,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.RegisteredPersonId,
                    opt => opt.Ignore());

            CreateMap<ChildInformationCreateModel,  RegisteredChild>()
                .IncludeBase<ChildInformationModelBase,  RegisteredChild>();

            CreateMap<RegistrationModelBase,  RegisteredPerson>()
                .ForMember(
                    dest => dest.RegistrationTimestamp,
                    opt => opt.UseValue(DateTimeOffset.Now))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Season,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.SeasonId,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Children,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PaymentDetails,
                    opt => opt.Ignore());
            
            CreateMap<RegistrationCreateModel,  RegisteredPerson>()
                .IncludeBase<RegistrationModelBase,  RegisteredPerson>()
                .ForMember(
                    dest => dest.Children,
                    opt => opt.MapFrom(src => src.Children.Select(Mapper.Map<RegisteredChild>)));

            CreateMap<RegisteredPerson, RegistrationReadModel>()
                .ForMember(
                    dest => dest.HasPaid,
                    opt => opt.MapFrom(src => src.PaymentDetails.Any()));

            CreateMap<RegisteredChild, ChildInformationReadModel>()
                .ForMember(
                    dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.RegisteredPersonId));;
        }
    }
}