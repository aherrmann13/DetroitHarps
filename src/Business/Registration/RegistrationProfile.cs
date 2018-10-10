namespace DetroitHarps.Business.Registration
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;

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
                    opt => opt.UseValue(DateTime.Now.Year))
                .ForMember(
                    dest => dest.PaymentInformation,
                    opt => opt.Ignore())
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