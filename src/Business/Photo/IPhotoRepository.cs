namespace DetroitHarps.Business.Photo
{
    using DetroitHarps.Business.Photo.Entities;

    public interface IPhotoRepository : IRepository<Photo, int>, IQueryableRepository<Photo, int>
    {
        void UpdateDisplayProperties(int id, PhotoDisplayProperties properties);

        bool PhotosExistWithGroupId(int groupId);
    }
}