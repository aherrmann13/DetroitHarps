namespace Business.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Models;

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<IEventModel, Event>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<EventModel, Event>();

            CreateMap<Event, EventModel>();
        }
    }
} 