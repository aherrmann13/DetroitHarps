namespace DetroitHarps.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using DetroitHarps.Business;
    using DetroitHarps.DataAccess.S3;
    using Tools;

    public abstract class S3RepositoryBase<T, I> : IRepository<T, I>
        where T : class, IHasId<I>
    {
        public S3RepositoryBase(IS3ObjectStore<T, I> store)
        {
            Guard.NotNull(store, nameof(store));

            Store = store;
        }

        protected IS3ObjectStore<T, I> Store { get; }

        public I Create(T entity)
        {
            Store.Put(entity).Wait();
            return entity.Id;
        }

        public void Update(T entity) => Store.Put(entity).Wait();

        public void Delete(I id) => Store.Delete(id).Wait();

        public bool Exists(I id) => GetSingleOrDefault(id) != default(T);

        public T GetSingleOrDefault(I id) => Store.Get(id).Result;

        // TODO: async?  also is this slow?  make parallel calls?
        public IList<T> GetAll() => Store.GetAllIds().Result.Select(x => Store.Get(x).Result).ToList();
    }
}