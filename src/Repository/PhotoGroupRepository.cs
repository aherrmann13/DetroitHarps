namespace DetroitHarps.Repository
{
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.DataAccess;

    public class PhotoGroupRepository : DbContextRepositoryBase<PhotoGroup, int>, IPhotoGroupRepository
    {
        public PhotoGroupRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}