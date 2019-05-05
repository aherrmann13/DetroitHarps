namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Common.Constants;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Tools;

    public class RegistrationManager : IRegistrationManager
    {
        private const double SingleChildAmountDue = 20;
        private const double MultipleChildAmountDue = 30;

        private readonly IRegistrationRepository _repository;
        private readonly IEventAccessor _eventAccessor;
        private readonly ILogger<RegistrationManager> _logger;

        public RegistrationManager(
            IRegistrationRepository repository,
            IEventAccessor eventAccessor,
            ILogger<RegistrationManager> logger)
        {
            Guard.NotNull(repository, nameof(repository));
            Guard.NotNull(eventAccessor, nameof(eventAccessor));
            Guard.NotNull(logger, nameof(logger));

            _eventAccessor = eventAccessor;
            _repository = repository;
            _logger = logger;
        }

        public void Register(RegisterModel model)
        {
            Guard.NotNull(model, nameof(model), Constants.NullExceptionGenerator);
            _logger.LogInformation($"new registration: {JsonConvert.SerializeObject(model)}");

            var entity = Mapper.Map<Registration>(model);
            entity.Children
                .SelectMany(x => x.Events)
                .ToList()
                .ForEach(x =>
                    x.EventSnapshot = _eventAccessor.GetSnapshot(x.EventId));

            SetPaymentAmount(entity);

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

        private static void SetPaymentAmount(Registration entity)
        {
            entity.PaymentInformation.Amount =
                entity.Children.Count == 1 ?
                    SingleChildAmountDue :
                    MultipleChildAmountDue;
        }
    }
}