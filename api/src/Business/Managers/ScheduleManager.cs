namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
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
            
            var entity = Mapper.Map<Event>(model);
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Update(EventUpdateModel model)
        {
            Guard.NotNull(model, nameof(model));
            
            ValidateEventExists(model.Id);

            var entity = Mapper.Map<Event>(model);
            _dbContext.Update(entity);
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
                .Select(Mapper.Map<EventReadModel>);
        
        public EventReadModel Get(int id) =>
            _dbContext.Set<Event>()
                .AsNoTracking()
                .Select(Mapper.Map<EventReadModel>)
                .First(x => x.Id.Equals(id));

        private void ValidateEventExists(int id)
        {
            if(!_dbContext.Set<Event>().Any(x => x.Id.Equals(id)))
            {
                throw new InvalidOperationException($"Event with id {id} does not exist");
            }
        }
    }
}