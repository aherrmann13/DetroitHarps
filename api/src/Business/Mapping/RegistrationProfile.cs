namespace Business.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Models;

    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegisterModel,  Registration>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PaymentInformation,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.SeasonYear,
                    opt => opt.UseValue(DateTime.Now.Year))
                .ForMember(
                    dest => dest.RegistrationTimestamp,
                    opt => opt.UseValue(DateTimeOffset.Now));
                
            CreateMap<IContactInformationModel, RegistrationContactInformation>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());
            
            CreateMap<IChildModel, RegistrationChild>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.Date));
            
            CreateMap<IParentModel, RegistrationParent>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<RegistrationChild, RegisteredChildModel>()
                .ForMember(
                    dest => dest.EmailAddress,
                    opt => opt.Ignore());
            
        }
    }
} 