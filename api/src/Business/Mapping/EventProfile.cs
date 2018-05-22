namespace Business.Mapping
{
    using System.Linq;
    using AutoMapper;
    using Business.Models;
    using Repository.Entities;

    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventModelBase,  Event>()
                .ForMember(
                    dest => dest.Date,
                    opt => opt.MapFrom(src => src.Date.DateTime))
                .ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore());

            CreateMap<EventCreateModel, Event>()
                .IncludeBase<EventModelBase,  Event>();

            // TODO : better way of mapping this
            CreateMap<EventUpdateModel, Event>()
                .IncludeBase<EventModelBase,  Event>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id));

            CreateMap<Event, EventReadModel>();
        }
    }
}