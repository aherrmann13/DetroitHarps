namespace DetroitHarps.Business.Photo
{
    using DetroitHarps.Business.Photo.Entities;

    public interface IPhotoRepository : IRepository<Photo>
    {
        void UpdateDisplayProperties(int id, PhotoDisplayProperties properties);
    }
}