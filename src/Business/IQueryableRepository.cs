namespace DetroitHarps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IQueryableRepository<T, I>
        where T : IHasId<I>
    {
        IList<T> GetMany(Expression<Func<T, bool>> filterClause);

        T GetSingleOrDefault(Expression<Func<T, bool>> filterClause);
    }
}