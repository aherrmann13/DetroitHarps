namespace Business.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Entities;
    using Business.Abstractions;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Tools;

    public class ScheduleManager : IScheduleManager
    {
        private readonly IEventRepository _eventRepository;

        public ScheduleManager(IEventRepository eventRepository)
        {
            Guard.NotNull(eventRepository, nameof(eventRepository));

            _eventRepository = eventRepository;
        }

        public int Create(EventCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<Event>(model);

            var id = _eventRepository.Create(entity);

            return id;
        }

        public void Update(EventModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<Event>(model);

            _eventRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _eventRepository.Delete(id);
        }

        public IEnumerable<EventModel> GetAll() =>
            _eventRepository.GetAll()
                .Select(Mapper.Map<EventModel>);

        public IEnumerable<EventModel> GetUpcoming(DateTime? untilDate = null)
        {
            var entities = _eventRepository.GetMany(
                x => x.Date >= DateTime.Now.ToUniversalTime().Date)?
                .Select(Mapper.Map<EventModel>);

            if(untilDate != null)
            {
                return entities.Where(x => x.Date <= untilDate);
            }
            else
            {
                return entities;
            }
        }
    }
}