namespace DetroitHarps.Business.Photo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class PhotoGroupManager : IPhotoGroupManager
    {
        private readonly IPhotoGroupRepository _photoGroupRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly ILogger<PhotoGroupManager> _logger;

        public PhotoGroupManager(
            IPhotoGroupRepository photoGroupRepository,
            IPhotoRepository photoRepository,
            ILogger<PhotoGroupManager> logger)
        {
            Guard.NotNull(photoGroupRepository, nameof(photoGroupRepository));
            Guard.NotNull(photoRepository, nameof(photoRepository));
            Guard.NotNull(logger, nameof(logger));

            _photoGroupRepository = photoGroupRepository;
            _photoRepository = photoRepository;
            _logger = logger;
        }

        public int Create(PhotoGroupCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"new photo group: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<PhotoGroup>(model);

            return _photoGroupRepository.Create(entity);
        }

        public void Update(PhotoGroupModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"updating photo group: {JsonConvert.SerializeObject(model)}");
            var entity = Mapper.Map<PhotoGroup>(model);

            ValidatePhotoGroupIdExists(model.Id);
            _photoGroupRepository.Update(entity);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"deleting photo group with id {id}");
            if (_photoRepository.PhotosExistWithGroupId(id))
            {
                throw new InvalidOperationException($"Photo group with id: {id} contains photos that must be deleted first");
            }

            _photoGroupRepository.Delete(id);
        }

        public IEnumerable<PhotoGroupModel> GetAll() =>
            _photoGroupRepository.GetAll()
                .Select(Mapper.Map<PhotoGroupModel>);

        public PhotoGroupModel Get(int id)
        {
            var entity = _photoGroupRepository.GetSingleOrDefault(id);

            return entity == null ? null : Mapper.Map<PhotoGroupModel>(entity);
        }

        private void ValidatePhotoGroupIdExists(int id)
        {
            if (!_photoGroupRepository.Exists(id))
            {
                throw new InvalidOperationException($"Photo group with id: {id} does not exist");
            }
        }
    }
}