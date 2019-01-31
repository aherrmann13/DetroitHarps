namespace DetroitHarps.Business.Registration
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;

    public class RegistrationProfile : Profile
    {
        private static readonly RegistrationPaymentInformation EmptyPayment =
            new RegistrationPaymentInformation();

        public RegistrationProfile()
        {
            CreateMap<RegisterModel,  Registration>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.SeasonYear,
                    opt => opt.UseValue(DateTime.Now.Year))
                .ForMember(
                    dest => dest.PaymentInformation,
                    opt => opt.UseValue(EmptyPayment))
                .ForMember(
                    dest => dest.RegistrationTimestamp,
                    opt => opt.UseValue(DateTimeOffset.Now));

            CreateMap<RegisterContactInformationModel, RegistrationContactInformation>();

            CreateMap<RegisterParentModel, RegistrationParent>();

            CreateMap<RegisterChildModel, RegistrationChild>();

            CreateMap<RegistrationChild, RegisteredChildModel>()
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
        }

        private class RegistrationChildConverter : ITypeConverter<Registration, IEnumerable<RegisteredChildModel>>
        {
            public IEnumerable<RegisteredChildModel> Convert(
                Registration source,
                IEnumerable<RegisteredChildModel> destination,
                ResolutionContext context)
            {
                foreach (var child in source.Children)
                {
                    var childModel = Mapper.Map<RegisteredChildModel>(child);
                    childModel.EmailAddress = source.ContactInformation.Email;
                    childModel.ParentFirstName = source.Parent.FirstName;
                    childModel.ParentLastName = source.Parent.LastName;

                    yield return childModel;
                }
            }
        }
    }
}