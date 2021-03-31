namespace DetroitHarps.DataAccess.S3
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DetroitHarps.Business;

    public interface IS3ObjectStore<T, I>
        where T : IHasId<I>
    {
        Task Put(T item);

        Task<T> Get(I id);

        Task Delete(I id);

        Task<IList<I>> GetAllIds();
    }
}
