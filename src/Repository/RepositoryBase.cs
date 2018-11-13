namespace DetroitHarps.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DetroitHarps.Business;
    using DetroitHarps.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Tools;

    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class, IHasId
    {
        protected RepositoryBase(DetroitHarpsDbContext dbContext)
        {
            // dont really think there is any need for a logger for now
            // dbContext can take care of logging - any derived class with
            // special logging needs can do that within that class
            Guard.NotNull(dbContext, nameof(dbContext));

            DbContext = dbContext;
        }

        protected DetroitHarpsDbContext DbContext { get; }

        protected abstract IQueryable<T> BaseQuery { get; }

        public int Create(T entity)
        {
            Guard.NotNull(entity, nameof(entity));
            DbContext.Add(entity);
            DbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(T entity)
        {
            Guard.NotNull(entity, nameof(entity));
            DbContext.Update(entity);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetSingleOrDefault(id);

            if (entity != null)
            {
                DbContext.Remove(entity);
                DbContext.SaveChanges();
            }
        }

        public bool Exists(int id) => DbContext.Set<T>().Any(x => x.Id == id);

        public IList<T> GetAll() => BaseQuery.ToList();

        public IList<T> GetMany(Expression<Func<T, bool>> filterClause)
        {
            Guard.NotNull(filterClause, nameof(filterClause));

            return BaseQuery.AsNoTracking().Where(filterClause).ToList();
        }

        public T GetSingleOrDefault(Expression<Func<T, bool>> filterClause)
        {
            Guard.NotNull(filterClause, nameof(filterClause));

            return BaseQuery.AsNoTracking().SingleOrDefault(filterClause);
        }

        public T GetSingleOrDefault(int id) => BaseQuery.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }
}