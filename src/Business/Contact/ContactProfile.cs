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
                    opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(
                    dest => dest.Timestamp,
                    opt => opt.MapFrom(val => DateTimeOffset.Now));

            CreateMap<Message, MessageReadModel>()
                .ForMember(
                    dest => dest.IsRead,
                    opt => opt.MapFrom(x => true));
        }
    }
}