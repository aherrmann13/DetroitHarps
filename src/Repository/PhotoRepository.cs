namespace DetroitHarps.Repository
{
    using System.Linq;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.DataAccess;
    using Tools;

    public class PhotoRepository : DbContextRepositoryBase<Photo, int>, IPhotoRepository
    {
        public PhotoRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }

        public void UpdateDisplayProperties(int id, PhotoDisplayProperties properties)
        {
            Guard.NotNull(properties, nameof(properties));

            // TODO: throw on not exist?
            // TODO: better way to unit test this
            if (Exists(id))
            {
                var photo = new Photo { Id = id, DisplayProperties = properties };
                DbContext.Attach(photo);
                DbContext.Update(photo.DisplayProperties);
                DbContext.Entry(photo).Reference(x => x.Data).IsModified = false;
                DbContext.SaveChanges();
            }
        }

        public bool PhotosExistWithGroupId(int groupId) =>
            DbContext.Set<PhotoDisplayProperties>().Any(x => x.PhotoGroupId == groupId);
    }
}