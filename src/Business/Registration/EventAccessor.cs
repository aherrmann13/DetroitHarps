namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using AutoMapper;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Entities;
    using Microsoft.Extensions.Logging;
    using Tools;

    public class EventAccessor : IEventAccessor
    {
        private readonly IEventRepository _repository;
        private readonly IDictionary<int, Event> _eventCache;

        public EventAccessor(IEventRepository repository)
        {
            Guard.NotNull(repository, nameof(repository));

            _repository = repository;
            _eventCache = new Dictionary<int, Event>();
        }

        public RegistrationChildEventSnapshot GetSnapshot(int eventId) =>
            Mapper.Map<RegistrationChildEventSnapshot>(GetOrThrow(eventId));

        public string GetName(int eventId) =>
            GetOrThrow(eventId).Title;

        private Event GetOrThrow(int eventId)
        {
            if (_eventCache.ContainsKey(eventId))
            {
                return _eventCache[eventId];
            }
            else
            {
                var entity = _repository.GetSingleOrDefault(eventId);
                if (entity == null)
                {
                    throw new BusinessException($"Event with id {eventId} doesn't exist");
                }

                _eventCache.Add(eventId, entity);
                return entity;
            }
        }
    }
}