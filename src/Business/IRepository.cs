namespace DetroitHarps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IRepository<T>
        where T : IHasId
    {
        int Create(T entity);

        void Update(T entity);

        void Delete(int id);

        IList<T> GetAll();

        IList<T> GetMany(Expression<Func<T, bool>> filterClause);

        T GetSingleOrDefault(Expression<Func<T, bool>> filterClause);

        T GetSingleOrDefault(int id);
    }
}