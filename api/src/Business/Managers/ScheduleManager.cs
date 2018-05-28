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

        public int Create(EventCreateModel model)
        {
            Guard.NotNull(model, nameof(model));
            var entity = CreateInternal(model);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Update(EventUpdateModel model)
        {
            Guard.NotNull(model, nameof(model));
            
            var entity = _dbContext.Set<Event>()
                .First(x => x.Id.Equals(model.Id));

            UpdateInternal(model, entity);

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Delete(int id)
        {
            var entity = _dbContext.Set<Event>()
                .First(x => x.Id.Equals(id));

            _dbContext.Remove(entity);

            _dbContext.SaveChanges();

            return entity.Id;
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
        

        public EventReadModel Get(int id) =>
            _dbContext.Set<Event>()
                .AsNoTracking()
                .Select(x => new EventReadModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Title = x.Title,
                    Description = x.Description
                })
                .First(x => x.Id.Equals(id));

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