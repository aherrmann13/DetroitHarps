namespace DetroitHarps.Business.Registration
{
    using AutoMapper;
    using DetroitHarps.Business.Common.Exceptions;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Schedule;
    using Microsoft.Extensions.Logging;
    using Tools;

    public class EventSnapshotProvider : IEventSnapshotProvider
    {
        private readonly IEventRepository _repository;

        public EventSnapshotProvider(IEventRepository repository)
        {
            Guard.NotNull(repository, nameof(repository));

            _repository = repository;
        }

        public RegistrationChildEventSnapshot GetSnapshot(int eventId)
        {
            var entity = _repository.GetSingleOrDefault(eventId);
            if (entity == null)
            {
                throw new BusinessException($"Event with id {eventId} doesn't exist");
            }

            return Mapper.Map<RegistrationChildEventSnapshot>(entity);
        }
    }
}