namespace Business.Managers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Entities;
    using Business.Abstractions;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Tools;

    public class PhotoManager : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoManager(IPhotoRepository photoRepository)
        {
            Guard.NotNull(photoRepository, nameof(photoRepository));

            _photoRepository = photoRepository;
        }

        public int Create(PhotoCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<PhotoDisplayProperties>(model);
            entity.PhotoData = Mapper.Map<PhotoData>(model);

            var id = _photoRepository.Create(entity);

            return id;
        }

        public void UpdateDisplayProperties(PhotoDisplayPropertiesModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<PhotoDisplayProperties>(model);

            _photoRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _photoRepository.Delete(id);
        }

        public IEnumerable<PhotoDisplayPropertiesModel> GetAll() =>
            _photoRepository.GetAll()
                .Select(Mapper.Map<PhotoDisplayPropertiesModel>);

        public IEnumerable<PhotoDisplayPropertiesModel> GetByGroupId(int groupId) =>
            _photoRepository.GetMany(x => x.PhotoGroupId.Equals(groupId))
                .Select(Mapper.Map<PhotoDisplayPropertiesModel>);


        public PhotoDisplayPropertiesModel Get(int id)
        {
            var entity =_photoRepository.GetSingleOrDefault(id);

            return entity == null ? null : Mapper.Map<PhotoDisplayPropertiesModel>(entity);
        }

        public PhotoDataModel GetPhotoBytes(int id)
        {
            var entity =_photoRepository.GetSingleOrDefault(id);

            return entity == null ? null : Mapper.Map<PhotoDataModel>(entity.PhotoData);
        }
    }
}