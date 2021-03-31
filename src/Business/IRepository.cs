namespace DetroitHarps.Business
{
    using System.Collections.Generic;

    public interface IRepository<T, I>
        where T : IHasId<I>
    {
        I Create(T entity);

        void Update(T entity);

        void Delete(I id);

        bool Exists(I id);

        T GetSingleOrDefault(I id);

        IList<T> GetAll();
    }
}