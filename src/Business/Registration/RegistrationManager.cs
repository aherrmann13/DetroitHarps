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
            _logger.LogInformation($"disabling registration with id {id}");
            var entity = _repository.GetSingleOrDefault(id);
            if (entity != null)
            {
                entity.IsDisabled = true;
                _repository.Update(entity);
            }
        }

        public void DeleteChild(int id, string firstName, string lastName)
        {
            _logger.LogInformation($"disabling {firstName} {lastName} in registration with id {id}");
            var entity = _repository.GetSingleOrDefault(id);
            var child = GetChild(entity, firstName, lastName);
            if (child != null)
            {
                child.IsDisabled = true;
                _repository.Update(entity);
            }
        }

        public IEnumerable<RegisteredParentModel> GetRegisteredParents(int year) =>
            _repository.GetMany(x => !x.IsDisabled && x.SeasonYear == year)
                .Select(Mapper.Map<RegisteredParentModel>);


        public IEnumerable<RegisteredChildModel> GetRegisteredChildren(int year) =>
            _repository.GetMany(x => !x.IsDisabled && x.SeasonYear == year)
                .SelectMany(Mapper.Map<IEnumerable<RegisteredChildModel>>);

        private RegistrationChild GetChild(
            Registration registration,
            string firstName,
            string lastName) =>
            registration?.Children?.FirstOrDefault(x =>
                Compare.EqualOrdinal(x.FirstName, firstName) &&
                Compare.EqualOrdinal(x.LastName, lastName));

        private static void SetPaymentAmount(Registration entity)
        {
            entity.PaymentInformation.Amount =
                entity.Children.Count == 1 ?
                    SingleChildAmountDue :
                    MultipleChildAmountDue;
        }
    }
}