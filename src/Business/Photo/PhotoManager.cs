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
        private readonly IPhotoRepository _repository;
        private readonly ILogger<PhotoManager> _logger;

        public PhotoManager(IPhotoRepository repository, ILogger<PhotoManager> logger)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(logger, nameof(logger));

            _repository = repository;
            _logger = logger;
        }

        public int Create(PhotoModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"new photo with display properties: {JsonConvert.SerializeObject(model.DisplayProperties)}");

            var entity = Mapper.Map<Photo>(model);
            var id = _repository.Create(entity);
            return id;
        }

        public void UpdateDisplayProperties(PhotoDisplayPropertiesDetailModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"updating photo with display properties: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<PhotoDisplayProperties>(model);

            _repository.UpdateDisplayProperties(model.PhotoId, entity);
        }

        public void Delete(int id) => _repository.Delete(id);

        public IEnumerable<PhotoDisplayPropertiesDetailModel> GetAll() =>
            _repository.GetAll()
                .Select(Mapper.Map<PhotoDisplayPropertiesDetailModel>);

        public PhotoDataModel GetPhotoBytes(int id)
        {
            var entity = _repository.GetSingleOrDefault(id);

            // TODO: better mapping (entity -> PhotoDataModel)
            return entity == null ? null : Mapper.Map<PhotoDataModel>(entity.Data);
        }
    }
}