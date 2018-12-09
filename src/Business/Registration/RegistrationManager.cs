namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Constants;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class RegistrationManager : IRegistrationManager
    {
        private readonly IRegistrationRepository _repository;
        private readonly ILogger<RegistrationManager> _logger;

        public RegistrationManager(IRegistrationRepository repository, ILogger<RegistrationManager> logger)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(logger, nameof(logger));

            _repository = repository;
            _logger = logger;
        }

        public void Register(RegisterModel model)
        {
            Guard.NotNull(model, nameof(model), Constants.NullExceptionGenerator);
            _logger.LogInformation($"new registration: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<Registration>(model);

            _repository.Create(entity);
        }

        public void Delete(int id)
        {
            _logger.LogInformation($"deleting registration with id {id}");
            _repository.Delete(id);
        }

        public IEnumerable<RegisteredParentModel> GetAllRegisteredParents() =>
            _repository.GetAll()
                .Select(Mapper.Map<RegisteredParentModel>);

        public IEnumerable<RegisteredChildModel> GetAllRegisteredChildren() =>
            _repository.GetAll()
                .SelectMany(Mapper.Map<IEnumerable<RegisteredChildModel>>);
    }
}