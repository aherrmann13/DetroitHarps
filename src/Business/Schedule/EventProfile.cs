namespace DetroitHarps.Business.Schedule
{
    using System;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Schedule.Entities;
    using DetroitHarps.Business.Schedule.Models;

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventCreateModel, Event>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<EventModel, Event>()
                .ReverseMap();
        }
    }
}