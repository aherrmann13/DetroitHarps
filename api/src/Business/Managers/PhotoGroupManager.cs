namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using DataAccess;
    using DataAccess.Entities;
    using Tools;

    public class PhotoGroupManager : IPhotoGroupManager
    {
        private readonly ApiDbContext _dbContext;

        public PhotoGroupManager(ApiDbContext dbContext)
        {
            Guard.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public int Create(PhotoGroupCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = CreateInternal(model);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Update(PhotoGroupUpdateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = _dbContext.Set<PhotoGroup>()
                .First(x => x.Id.Equals(model.Id));

            UpdateInternal(model, entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Delete(int id)
        {
            var entity = _dbContext.Set<PhotoGroup>()
                .First(x => x.Id.Equals(id));

            ValidateGroupContainsNoPhotos(id);

            _dbContext.Remove(entity);

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public IEnumerable<PhotoGroupReadModel> Get() =>
            _dbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .Select(x => new PhotoGroupReadModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SortOrder = x.SortOrder,
                        PhotoIds = x.Photos.Select(y => y.Id).ToList()
                    });

        public PhotoGroupReadModel Get(int id) =>
            _dbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .Where(x => x.Id.Equals(id))
                .Select(x => new PhotoGroupReadModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    PhotoIds = x.Photos.Select(y => y.Id).ToList()
                })
                .First();

        private PhotoGroup CreateInternal(PhotoGroupCreateModel model) =>
            new PhotoGroup
            {
                Name = model.Name,
                SortOrder = model.SortOrder
            };
        
        private void UpdateInternal(PhotoGroupUpdateModel model, PhotoGroup entity) 
        {
            entity.Name = model.Name;
            entity.SortOrder = model.SortOrder;
        }

        private void ValidateGroupContainsNoPhotos(int groupId)
        {
            if(_dbContext.Set<Photo>().Any(x => x.PhotoGroupId.Equals(x.Id)))
            {
                throw new InvalidOperationException($"Photo Group with id {groupId} has photos");
            }
        }
    }
}