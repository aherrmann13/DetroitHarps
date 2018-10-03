namespace DetroitHarps.Business.Photo
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.Business.Photo.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class PhotoManager : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly ILogger<PhotoManager> _logger;

        public PhotoManager(IPhotoRepository photoRepository, ILogger<PhotoManager> logger)
        {
            Guard.NotNull(photoRepository, nameof(photoRepository));
            Guard.NotNull(logger, nameof(logger));

            _photoRepository = photoRepository;
            _logger = logger;
        }

        public int Create(PhotoModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"new photo with display properties: {JsonConvert.SerializeObject(model.DisplayProperties)}");

            var entity = Mapper.Map<Photo>(model);
            var id = _photoRepository.Create(entity);
            return id;
        }

        public void UpdateDisplayProperties(PhotoDisplayPropertiesDetailModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"updating photo with display properties: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<PhotoDisplayProperties>(model);

            _photoRepository.UpdateDisplayProperties(model.PhotoId, entity);
        }

        public void Delete(int id) => _photoRepository.Delete(id);

        public IEnumerable<PhotoDisplayPropertiesDetailModel> GetAll() =>
            _photoRepository.GetAll()
                .Select(Mapper.Map<PhotoDisplayPropertiesDetailModel>);

        public PhotoDataModel GetPhotoBytes(int id)
        {
            var entity = _photoRepository.GetSingleOrDefault(id);

            // TODO: better mapping (entity -> PhotoDataModel)
            return entity == null ? null : Mapper.Map<PhotoDataModel>(entity.Data);
        }
    }
}