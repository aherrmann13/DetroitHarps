namespace DetroitHarps.Business.Photo
{
    using DetroitHarps.Business.Photo.Entities;

    public interface IPhotoGroupRepository : IRepository<PhotoGroup, int>, IQueryableRepository<PhotoGroup, int>
    {
    }
}