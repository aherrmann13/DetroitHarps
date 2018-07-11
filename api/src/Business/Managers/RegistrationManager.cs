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

    public class RegistrationManager : IRegistrationManager
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationManager(IRegistrationRepository registrationRepository)
        {
            Guard.NotNull(registrationRepository, nameof(registrationRepository));

            _registrationRepository = registrationRepository;
        }

        public void Register(RegisterModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = Mapper.Map<Registration>(model);

            _registrationRepository.Create(entity);
        }

        public void AddChild(RegisterAddChildModel model)
        {
            Guard.NotNull(model, nameof(model));

            var entity = _registrationRepository.GetSingleOrDefault(x =>
                Compare.EqualOrdinal(x.Parent.FirstName, model.ParentFirstName) &&
                Compare.EqualOrdinal(x.Parent.LastName, model.ParentLastName));

            if(entity == null)
            {
                // TODO this should return nicely to the user not throw?
                // TODO is this a responsibility of the repository?
                throw new InvalidOperationException("Parent not found");
            }

            entity.Children.Add(Mapper.Map<IChildModel, RegistrationChild>(model));

            _registrationRepository.Update(entity);
        }

        public IEnumerable<RegisteredChildModel> GetAllRegisteredChildren() =>
            _registrationRepository.GetAll()
                .SelectMany(x => 
                {
                    var models = x.Children.Select(Mapper.Map<RegisteredChildModel>);
                    
                    models = models.ForEach(y => y.EmailAddress = x.ContactInformation?.Email ?? string.Empty);

                    return models;
                });
                
    }
}