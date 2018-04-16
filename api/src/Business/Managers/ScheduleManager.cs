namespace Business.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Entities;
    using Tools;

    public class ScheduleManager : IScheduleManager
    {
        private readonly ApiDbContext _dbContext;
        public ScheduleManager(ApiDbContext dbContext)
        {
            Guard.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public IEnumerable<int> Create(params EventCreateModel[] models)
        {
            if(models == null || models.Length == 0)
            {
                return new List<int>();
            }

            var events = models.Where(x => x != null).Select(CreateInternal).ToList();

            _dbContext.AddRange(events);
            _dbContext.SaveChanges();

            return events.Select(x => x.Id);
        }

        public IEnumerable<int> Update(params EventUpdateModel[] models)
        {
            if(models == null || models.Length == 0)
            {
                return new List<int>();
            }
            var eventIds = models.Where(x => x != null).Select(x => x.Id);

            var entities = _dbContext.Set<Event>()
                .Where(x => eventIds.Contains(x.Id))
                .ToList();

            entities.ForEach(x => UpdateInternal(models.First(y => y.Id.Equals(x.Id)), x));

            _dbContext.SaveChanges();

            return entities.Select(x => x.Id);
        }

        public IEnumerable<int> Delete(params int[] ids)
        {
            if(ids == null || ids.Length == 0)
            {
                return new List<int>();
            }

            var entities = _dbContext.Set<Event>()
                .Where(x => ids.Contains(x.Id))
                .ToList();

            _dbContext.RemoveRange(entities);

            _dbContext.SaveChanges();

            return entities.Select(x => x.Id);
        }

        public IEnumerable<EventReadModel> GetAll() =>
            _dbContext.Set<Event>()
                .AsNoTracking()
                .Select(x => new EventReadModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Title = x.Title,
                    Description = x.Description
                });
        

        public IEnumerable<EventReadModel> Get(params int[] ids) =>
            _dbContext.Set<Event>()
                .AsNoTracking()
                .Where(x => ids != null && ids.Contains(x.Id))
                .Select(x => new EventReadModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Title = x.Title,
                    Description = x.Description
                });

        private Event CreateInternal(EventCreateModel model) =>
            new Event
            {
                Date = model.Date.Date,
                Title = model.Title,
                Description = model.Description,
            };

        private Event UpdateInternal(EventUpdateModel model, Event entity)
        {
            entity.Date = model.Date.Date;
            entity.Title = model.Title;
            entity.Description = model.Description;

            return entity;
        }
    }
}