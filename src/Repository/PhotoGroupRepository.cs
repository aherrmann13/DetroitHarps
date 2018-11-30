namespace DetroitHarps.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using DetroitHarps.Business;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.DataAccess;

    public class PhotoGroupRepository : RepositoryBase<PhotoGroup>, IPhotoGroupRepository
    {
        public PhotoGroupRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}