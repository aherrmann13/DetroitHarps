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
        private readonly IPhotoGroupRepository _repository;
        private readonly ILogger<PhotoGroupManager> _logger;

        public PhotoGroupManager(IPhotoGroupRepository repository, ILogger<PhotoGroupManager> logger)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(logger, nameof(logger));

            _repository = repository;
            _logger = logger;
        }

        public int Create(PhotoGroupCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"new photo group: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<PhotoGroup>(model);

            return _repository.Create(entity);
        }

        public void Update(PhotoGroupModel model)
        {
            Guard.NotNull(model, nameof(model));

            _logger.LogInformation($"updating photo group: {JsonConvert.SerializeObject(model)}");
            var entity = Mapper.Map<PhotoGroup>(model);

            ValidatePhotoGroupIdExists(model.Id);
            _repository.Update(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IEnumerable<PhotoGroupModel> GetAll() =>
            _repository.GetAll()
                .Select(Mapper.Map<PhotoGroupModel>);

        public PhotoGroupModel Get(int id)
        {
            var entity = _repository.GetSingleOrDefault(id);

            return entity == null ? null : Mapper.Map<PhotoGroupModel>(entity);
        }

        private void ValidatePhotoGroupIdExists(int id)
        {
            if (!_repository.Exists(id))
            {
                throw new InvalidOperationException($"Photo group with id: {id} does not exist");
            }
        }
    }
}