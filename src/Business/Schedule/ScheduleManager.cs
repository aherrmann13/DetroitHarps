namespace DetroitHarps.Business.Schedule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Schedule.Entities;
    using DetroitHarps.Business.Schedule.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class ScheduleManager : IScheduleManager
    {
        private readonly IEventRepository _repository;
        private readonly ILogger<ScheduleManager> _logger;

        public ScheduleManager(IEventRepository repository, ILogger<ScheduleManager> logger)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(logger, nameof(logger));

            _repository = repository;
            _logger = logger;
        }

        public int Create(EventCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"new event: {JsonConvert.SerializeObject(model)}");
            var entity = Mapper.Map<Event>(model);

            return _repository.Create(entity);
        }

        public void Update(EventModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"updating photo group: {JsonConvert.SerializeObject(model)}");
            var entity = Mapper.Map<Event>(model);

            ValidateEventExists(model.Id);
            _repository.Update(entity);
        }

        public void Delete(int id) => _repository.Delete(id);

        public IEnumerable<EventModel> GetAll() =>
            _repository.GetAll()
                .Select(Mapper.Map<EventModel>);

        public IEnumerable<EventModel> GetUpcoming(DateTime? untilDate = null)
        {
            var entities = _repository.GetMany(
                x => x.Date >= DateTime.Now.ToUniversalTime().Date)?
                .Select(Mapper.Map<EventModel>);

            return untilDate == null ? entities : entities.Where(x => x.Date <= untilDate);
        }

        private void ValidateEventExists(int id)
        {
            if (!_repository.Exists(id))
            {
                throw new InvalidOperationException($"Event with id: {id} does not exist");
            }
        }
    }
}