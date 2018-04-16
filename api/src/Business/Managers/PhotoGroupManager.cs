namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Entities;
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
            // TODO null or fail silently?
            // TODO make this behavior consistant
            if(model == null)
            {
                throw new ArgumentNullException("cannot post null model");
            }

            var entity = CreateInternal(model);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Update(PhotoGroupUpdateModel model)
        {
            // TODO null or fail silently?
            // TODO make this behavior consistant
            if(model == null)
            {
                throw new ArgumentNullException("cannot post null model");
            }

            var entity = _dbContext
                .Set<PhotoGroup>()
                .First(x => x.Id.Equals(model.Id));

            if(entity == null)
            {
                throw new InvalidOperationException($"Entity with id {model.Id} does not exist");
            }

            UpdateInternal(model, entity);

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Delete(int id)
        {
            var entity = _dbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .First(x => id.Equals(x.Id));

            var relatedEntities = _dbContext.Set<Photo>()
                .AsNoTracking()
                .Any(x => x.PhotoGroupId.Equals(x.Id));

            if(relatedEntities)
            {
                throw new InvalidOperationException($"PhotoGroup with id {id} has photos");
            }

            _dbContext.Remove(entity);

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public IEnumerable<PhotoGroupReadModel> GetAll() =>
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
    }
}