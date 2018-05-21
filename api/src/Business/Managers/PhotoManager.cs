namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Entities;
    using Tools;

    public class PhotoManager : IPhotoManager
    {
        private readonly ApiDbContext _dbContext;

        public PhotoManager(ApiDbContext dbContext)
        {
            Guard.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }

        public int Create(PhotoCreateModel model)
        {
            Guard.NotNull(model, nameof(model));
            ValidatePhotoGroupExists(model.GroupId);
            // TODO : virus scan
            // for now, only one (known) user will be provided a login
            var entity = Mapper.Map<Photo>(model);
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            
            return entity.Id;
        }

        public int Update(PhotoMetadataUpdateModel model)
        {
            Guard.NotNull(model, nameof(model));

            ValidatePhotoExists(model.Id);
            ValidatePhotoGroupExists(model.GroupId);

            var entity = Mapper.Map<Photo>(model);
            _dbContext.Update(entity);

            // TODO : Photo bytes should be seperate table
            _dbContext.Entry(entity).Property(x => x.Data).IsModified = false;
            _dbContext.SaveChanges();

            return model.Id;
        }

        public int Delete(int id)
        {
            _dbContext.Remove(_dbContext.Set<Photo>().First(x => x.Id.Equals(id)));
            _dbContext.SaveChanges();

            return id;
        }

        public IEnumerable<PhotoMetadataReadModel> GetMetadata()=>
            _dbContext.Set<Photo>()
                .AsNoTracking()
                .Select(Mapper.Map<PhotoMetadataReadModel>);

        public PhotoMetadataReadModel GetMetadata(int id) =>
            _dbContext.Set<Photo>()
                .Where(x => x.Id.Equals(id))
                .Select(Mapper.Map<PhotoMetadataReadModel>)
                .First();

        public PhotoReadModel Get(int id) =>
            _dbContext.Set<Photo>()
                .AsNoTracking()
                .Where(x => x.Id.Equals(id))
                .Select(Mapper.Map<PhotoReadModel>)
                .First();

        private void ValidatePhotoExists(int id)
        {
            if(!_dbContext.Set<Photo>().Any(x => x.Id.Equals(id)))
            {
                throw new InvalidOperationException($"Photo with id {id} does not exist");
            }
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