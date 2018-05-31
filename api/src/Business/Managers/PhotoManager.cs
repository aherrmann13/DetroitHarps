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
    using DataAccess;
    using DataAccess.Entities;
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
            Guard.NotNull(model, nameof(model));
            ValidatePhotoGroupExists(model.GroupId);

            // TODO : virus scan
            // for now, only one (known) user will be provided a login
            var entity = CreateInternal(model);
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            
            return entity.Id;
        }

        public int Update(PhotoMetadataUpdateModel model)
        {
            Guard.NotNull(model, nameof(model));
                       
            var entity = _dbContext.Set<Photo>()
                .First(x => x.Id.Equals(model.Id));

            ValidatePhotoGroupExists(model.GroupId);

            UpdateInternal(model, entity);
            _dbContext.SaveChanges();

            return model.Id;
        }

        public int Delete(int id)
        {
            var entity = _dbContext.Set<Photo>()
                .First(x => x.Id.Equals(id));

            _dbContext.Remove(entity);

            _dbContext.SaveChanges();

            return id;
        }

        public IEnumerable<PhotoMetadataReadModel> GetMetadata()=>
            _dbContext.Set<Photo>()
                .Select(x => new PhotoMetadataReadModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    GroupId = x.PhotoGroupId,
                    SortOrder = x.SortOrder
                });

        public PhotoMetadataReadModel GetMetadata(int id) =>
            _dbContext.Set<Photo>()
                .Select(x => new PhotoMetadataReadModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    GroupId = x.PhotoGroupId,
                    SortOrder = x.SortOrder
                })
                .First(x => x.Id.Equals(id));

        public PhotoReadModel Get(int id) =>
            _dbContext.Set<Photo>()
                .AsNoTracking()
                .Select(x => new PhotoReadModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    GroupId = x.PhotoGroupId,
                    SortOrder = x.SortOrder,
                    Photo = x.Data
                })
                .First(x => x.Id.Equals(id));
            
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

        private void ValidatePhotoGroupExists(int groupId)
        {
            if(!_dbContext.Set<PhotoGroup>().Any(x => x.Id.Equals(groupId)))
            {
                throw new InvalidOperationException($"Photo Group with id {groupId} does not exist");
            }
        }
    }
}