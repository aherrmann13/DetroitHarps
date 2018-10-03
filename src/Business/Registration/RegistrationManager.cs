namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class RegistrationManager : IRegistrationManager
    {
        private readonly IRegistrationRepository _registrationRepository;
        private readonly ILogger<RegistrationManager> _logger;

        public RegistrationManager(IRegistrationRepository registrationRepository, ILogger<RegistrationManager> logger)
        {
            Guard.NotNull(registrationRepository, nameof(registrationRepository));
            Guard.NotNull(logger, nameof(logger));

            _registrationRepository = registrationRepository;
            _logger = logger;
        }

        public void Register(RegisterModel model)
        {
            Guard.NotNull(model, nameof(model));
            _logger.LogInformation($"new registration: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<Registration>(model);

            _registrationRepository.Create(entity);
        }

        public IEnumerable<RegisteredChildModel> GetAllRegisteredChildren() =>
            _registrationRepository.GetAll()
                .SelectMany(Mapper.Map<IEnumerable<RegisteredChildModel>>);
    }
}