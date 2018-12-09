namespace DetroitHarps.Business.Photo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Constants;
    using DetroitHarps.Business.Exception;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class PhotoManager : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IPhotoGroupRepository _photoGroupRepository;
        private readonly ILogger<PhotoManager> _logger;

        public PhotoManager(
            IPhotoRepository photoRepository,
            IPhotoGroupRepository photoGroupRepository,
            ILogger<PhotoManager> logger)
        {
            Guard.NotNull(photoRepository, nameof(photoRepository));
            Guard.NotNull(photoGroupRepository, nameof(photoGroupRepository));
            Guard.NotNull(logger, nameof(logger));

            _photoRepository = photoRepository;
            _photoGroupRepository = photoGroupRepository;
            _logger = logger;
        }

        public int Create(PhotoModel model)
        {
            Guard.NotNull(model, nameof(model), Constants.NullExceptionGenerator);

            _logger.LogInformation($"new photo with display properties: {JsonConvert.SerializeObject(model.DisplayProperties)}");

            ValidatePhotoGroupExists(model.DisplayProperties.PhotoGroupId);
            var entity = Mapper.Map<Photo>(model);

            return _photoRepository.Create(entity);
        }

        public void UpdateDisplayProperties(PhotoDisplayPropertiesDetailModel model)
        {
            Guard.NotNull(model, nameof(model), Constants.NullExceptionGenerator);

            _logger.LogInformation($"updating photo with display properties: {JsonConvert.SerializeObject(model)}");

            ValidatePhotoExists(model.PhotoId);
            ValidatePhotoGroupExists(model.PhotoGroupId);
            var entity = Mapper.Map<PhotoDisplayProperties>(model);

            _photoRepository.UpdateDisplayProperties(model.PhotoId, entity);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"deleting photo with id {id}");
            _photoRepository.Delete(id);
        }

        public IEnumerable<PhotoDisplayPropertiesDetailModel> GetAll() =>
            _photoRepository.GetAll()
                .Select(Mapper.Map<PhotoDisplayPropertiesDetailModel>);

        public PhotoDataModel GetPhotoBytes(int id)
        {
            var entity = _photoRepository.GetSingleOrDefault(id);

            // TODO: better mapping (entity -> PhotoDataModel)
            return entity == null ? null : Mapper.Map<PhotoDataModel>(entity.Data);
        }

        private void ValidatePhotoExists(int id)
        {
            if (!_photoRepository.Exists(id))
            {
                throw new BusinessException($"Photo with id: {id} does not exist");
            }
        }

        private void ValidatePhotoGroupExists(int id)
        {
            if (!_photoGroupRepository.Exists(id))
            {
                throw new BusinessException($"Photo group with id: {id} does not exist");
            }
        }
    }
}