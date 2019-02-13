namespace DetroitHarps.Business.Contact
{
    using System;
    using AutoMapper;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.Business.Contact.Models;

    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<MessageModel, Message>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Timestamp,
                    opt => opt.MapFrom(val => DateTimeOffset.Now))
                .ForMember(
                    dest => dest.IsRead,
                    opt => opt.MapFrom(x => false));

            CreateMap<Message, MessageReadModel>();
        }
    }
}