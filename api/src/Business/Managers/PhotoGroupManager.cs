namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Business.Entities;
    using Business.Abstractions;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Tools;

    public class PhotoGroupManager : IPhotoGroupManager
    {
        private readonly IPhotoGroupRepository _photoGroupRepository;

        public PhotoGroupManager(IPhotoGroupRepository photoGroupRepository)
        {
            Guard.NotNull(photoGroupRepository, nameof(photoGroupRepository));

            _photoGroupRepository = photoGroupRepository;
        }

        public int Create(PhotoGroupCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<PhotoGroup>(model);

            var id = _photoGroupRepository.Create(entity);

            return id;
        }

        public void Update(PhotoGroupModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<PhotoGroup>(model);

            _photoGroupRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _photoGroupRepository.Delete(id);
        }

        public IEnumerable<PhotoGroupModel> GetAll() =>
            _photoGroupRepository.GetAll()
                .Select(Mapper.Map<PhotoGroupModel>);

        public PhotoGroupModel Get(int id)
        {
            var entity =_photoGroupRepository.GetSingleOrDefault(id);

            return entity == null ? null : Mapper.Map<PhotoGroupModel>(entity);
        }
    }
}