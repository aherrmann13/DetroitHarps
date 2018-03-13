namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Entities;
    using Tools;

    public class RegistrationManager : IRegistrationManager
    {
        private readonly ApiDbContext _dbContext;
        private int _currentYear;

        public RegistrationManager(ApiDbContext dbContext)
        {
            Guard.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
            _currentYear = DateTime.Now.Year;
        }

        public IEnumerable<int> Register(params RegistrationCreateModel[] models)
        {
            var season = _dbContext.Set<Season>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Year.Equals(_currentYear));

            if(models == null || models.Length == 0 || season == null)
            {
                return new List<int>();
            }

            var registeredPersons = models
                .Where(x => x != null)
                .Select(x => Create(x, season.Id))
                .ToList();

            _dbContext.AddRange(registeredPersons);
            _dbContext.SaveChanges();

            return registeredPersons.Select(x => x.Id);
        }

        public IEnumerable<RegistrationReadModel> GetAll() =>
            _dbContext.Set<RegisteredPerson>()
                .AsNoTracking()
                .Include(x => x.PaymentDetails)
                .Where(x => x.Season.Year.Equals(_currentYear))
                .Select(Create);

        public IEnumerable<ChildInformationReadModel> GetAllChildren() =>
            _dbContext.Set<RegisteredChild>()
                .AsNoTracking()
                .Where(x => x.RegisteredPerson.Season.Year.Equals(_currentYear))
                .Select(Create);

        private static RegisteredPerson Create(RegistrationCreateModel model, int seasonId) =>
            new RegisteredPerson
            {
                SeasonId = seasonId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Address2 = model.Address2,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
                Children = model.Children.Select(Create).ToList()
            };

        private static RegisteredChild Create(ChildInformationCreateModel model) =>
            new RegisteredChild
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.Date,
                ShirtSize = model.ShirtSize
            };

        private static RegistrationReadModel Create(RegisteredPerson entity) =>
            new RegistrationReadModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                EmailAddress = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address,
                Address2 = entity.Address2,
                City = entity.City,
                State = entity.State,
                Zip = entity.Zip,
                HasPaid = entity.PaymentDetails.Any()
            };

        private static ChildInformationReadModel Create(RegisteredChild entity) =>
            new ChildInformationReadModel
            {
                ParentId = entity.RegisteredPersonId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                ShirtSize = entity.ShirtSize
            };
    }
}