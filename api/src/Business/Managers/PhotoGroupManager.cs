namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
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
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<PhotoGroupCreateModel, PhotoGroup>(model);
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity.Id;
        }

        public int Update(PhotoGroupUpdateModel model)
        {
            Guard.NotNull(model, nameof(model));

            ValidateGroupExists(model.Id);

            var entity = Mapper.Map<PhotoGroup>(model);
            _dbContext.Update(entity);
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
                .Include(x => x.Photos)
                .Select(Mapper.Map<PhotoGroupReadModel>);

        public PhotoGroupReadModel Get(int id) =>
            _dbContext.Set<PhotoGroup>()
                .AsNoTracking()
                .Include(x => x.Photos)
                .Where(x => x.Id.Equals(id))
                .Select(Mapper.Map<PhotoGroupReadModel>)
                .First();

        private void ValidateGroupExists(int groupId)
        {
            if(!_dbContext.Set<PhotoGroup>().Any(x => x.Id.Equals(groupId)))
            {
                throw new InvalidOperationException($"Photo Group with id {groupId} does not exist");
            }
        }

        private void ValidateGroupContainsNoPhotos(int groupId)
        {
            if(_dbContext.Set<Photo>().Any(x => x.PhotoGroupId.Equals(groupId)))
            {
                throw new InvalidOperationException($"Photo Group with id {groupId} has photos");
            }
        }
    }
}