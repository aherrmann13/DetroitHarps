namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Entities;
    using Tools;

    public class PhotoManager : IPhotoManager
    {
        private readonly ApiDbContext _dbContext;
        private int _currentYear;

        public PhotoManager(ApiDbContext dbContext)
        {
            Guard.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
            _currentYear = DateTime.Now.Year;
        }

        public int Create(PhotoCreateModel model)
        {
            // TODO null or fail silently?
            // TODO make this behavior consistant
            if(model == null)
            {
                throw new ArgumentNullException("cannot post null model");
            }

            if(!_dbContext.Set<PhotoGroup>().Any(x => x.Id.Equals(model.GroupId)))
            {
                throw new InvalidOperationException("Photo group does not exist");
            }

            // TODO : virus scan
            // for now, only one (known) user will be provided a login
            var entity = CreateInternal(model);
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            
            return entity.Id;
        }

        public IEnumerable<int> Update(params PhotoMetadataUpdateModel[] models)
        {
            // TODO null or fail silently?
            // TODO make this behavior consistant
            if(models == null)
            {
                throw new ArgumentNullException("cannot post null models");
            }

            models = models.Where(x => x != null).ToArray();

            var ids = models.Select(x => x.Id).Distinct();
            var groupIds = models.Select(x => x.GroupId).Distinct();

            var existingPhotoMetadata = _dbContext.Set<Photo>()
                .Where(x => ids.Contains(x.Id))
                .ToList();

            var existingGroupIds = _dbContext.Set<PhotoGroup>()
                .Where(x => groupIds.Contains(x.Id))
                .Select(x => x.Id)
                .ToList();

            if(!groupIds.All(x => existingGroupIds.Contains(x)))
            {
                throw new InvalidOperationException("not all group ids exist");
            }

            existingPhotoMetadata.ForEach(x => 
                UpdateInternal(models.First(y => y.Id.Equals(x.Id)), x));

            _dbContext.SaveChanges();

            return existingPhotoMetadata.Select(x => x.Id);
        }

        public IEnumerable<int> Delete(params int[] ids)
        {
            // TODO null or fail silently?
            // TODO make this behavior consistant
            if(ids == null)
            {
                throw new ArgumentNullException("cannot post null models");
            }

             var entities = _dbContext.Set<Photo>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .ToList();

            _dbContext.RemoveRange(entities);

            _dbContext.SaveChanges();

            return entities.Select(x => x.Id);
        }

        public IEnumerable<PhotoMetadataReadModel> GetAll()=>
            _dbContext.Set<Photo>()
                .AsNoTracking()
                .Select(x => new PhotoMetadataReadModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    GroupId = x.PhotoGroupId,
                    SortOrder = x.SortOrder
                });

        public IEnumerable<PhotoMetadataReadModel> Get(params int[] ids)
        {
            // TODO null or fail silently?
            // TODO make this behavior consistant
            if(ids == null)
            {
                throw new ArgumentNullException("cannot post null models");
            }
            
            return _dbContext.Set<Photo>()
                .AsNoTracking()
                .Where(x => ids.Contains(x.Id))
                .Select(x => new PhotoMetadataReadModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    GroupId = x.PhotoGroupId,
                    SortOrder = x.SortOrder
                });
        }

        public PhotoReadModel GetSingle(int id)
        {
            var entity = _dbContext.Set<Photo>()
                .AsNoTracking()
                .First(x => x.Id.Equals(id));

            return new PhotoReadModel
            {
                Id = entity.Id,
                Title = entity.Title,
                GroupId = entity.PhotoGroupId,
                SortOrder = entity.SortOrder,
                Photo = entity.Data
            };
        }
            
        private Photo CreateInternal(PhotoCreateModel model) =>
            new Photo
            {
                PhotoGroupId = model.GroupId,
                Title = model.Title,
                SortOrder = model.SortOrder,
                Data = model.Photo
                
            };

        private void UpdateInternal(PhotoMetadataUpdateModel model, Photo entity) 
        {
            entity.PhotoGroupId = model.GroupId;
            entity.Title = model.Title;
            entity.SortOrder = model.SortOrder;
        }
    }
}