namespace DetroitHarps.Business.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using DetroitHarps.Business.Schedule.Entities;

    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegisterModel,  Registration>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.SeasonYear,
                    opt => opt.MapFrom(x => DateTime.Now.Year))
                .ForMember(
                    dest => dest.IsDisabled,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.PaymentInformation,
                    opt => opt.MapFrom(x => x.Payment))
                .ForMember(
                    dest => dest.RegistrationTimestamp,
                    opt => opt.MapFrom(x => DateTimeOffset.Now));

            CreateMap<RegisterPaymentModel, RegistrationPaymentInformation>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.Ignore());

            CreateMap<RegisterContactInformationModel, RegistrationContactInformation>();

            CreateMap<RegisterParentModel, RegistrationParent>();

            CreateMap<RegisterChildModel, RegistrationChild>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.IsDisabled,
                    opt => opt.Ignore());

            CreateMap<RegistrationChild, RegisteredChildModel>()
                .ForMember(
                    dest => dest.RegistrationId,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.ParentFirstName,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.ParentLastName,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.EmailAddress,
                    opt => opt.Ignore());

            CreateMap<Registration, IEnumerable<RegisteredChildModel>>()
                .ConvertUsing<RegistrationChildConverter>();

            CreateMap<Registration, RegisteredParentModel>()
                .ForMember(
                    dest => dest.RegistrationId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => src.Parent.FirstName))
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => src.Parent.LastName))
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.ContactInformation.Email))
                .ForMember(
                    dest => dest.ChildCount,
                    opt => opt.MapFrom(src => src.Children.Count));

            CreateMap<RegisterChildEventModel, RegistrationChildEvent>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.EventSnapshot,
                    opt => opt.Ignore());

            CreateMap<RegistrationChildEvent, RegisteredChildEventModel>();

            CreateMap<Event, RegistrationChildEventSnapshot>();
        }

        private class RegistrationChildConverter : ITypeConverter<Registration, IEnumerable<RegisteredChildModel>>
        {
            public IEnumerable<RegisteredChildModel> Convert(
                Registration source,
                IEnumerable<RegisteredChildModel> destination,
                ResolutionContext context)
            {
                // TODO: dont like this filtering here when it is done outside automapper on parent
                foreach (var child in source.Children.Where(x => !x.IsDisabled))
                {
                    var childModel = Mapper.Map<RegisteredChildModel>(child);
                    childModel.EmailAddress = source.ContactInformation.Email;
                    childModel.ParentFirstName = source.Parent.FirstName;
                    childModel.ParentLastName = source.Parent.LastName;
                    childModel.RegistrationId = source.Id;

                    yield return childModel;
                }
            }
        }
    }
}