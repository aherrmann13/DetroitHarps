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
        private readonly IStripeManager _stripeManager;
        private int _currentYear;

        public RegistrationManager(ApiDbContext dbContext, IStripeManager stripeManager)
        {
            Guard.NotNull(dbContext, nameof(dbContext));
            Guard.NotNull(stripeManager, nameof(stripeManager));

            _dbContext = dbContext;
            _stripeManager = stripeManager;
            _currentYear = DateTime.Now.Year;
        }

        public int Register(RegistrationCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var season = _dbContext.Set<Season>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Year.Equals(_currentYear));

            var registeredPerson = CreateInternal(model, season.Id);

            var stripeChargeModel = CreateStripeCharge(model);
            var customerId = _stripeManager.Charge(stripeChargeModel);
            var paymentDetails = CreateChargeRecord(stripeChargeModel, customerId);

            registeredPerson.PaymentDetails = new List<PaymentDetails> { paymentDetails };

            _dbContext.Add(registeredPerson);
            _dbContext.SaveChanges();

            return registeredPerson.Id;
        }

        public IEnumerable<RegistrationReadModel> GetAll() =>
            _dbContext.Set<RegisteredPerson>()
                .AsNoTracking()
                .Include(x => x.PaymentDetails)
                .Where(x => x.Season.Year.Equals(_currentYear))
                .Select(CreateInternal);

        public IEnumerable<ChildInformationReadModel> GetAllChildren() =>
            _dbContext.Set<RegisteredChild>()
                .AsNoTracking()
                .Where(x => x.RegisteredPerson.Season.Year.Equals(_currentYear))
                .Select(CreateInternal);

        private static RegisteredPerson CreateInternal(RegistrationCreateModel model, int seasonId) =>
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
                Children = model.Children.Select(CreateInternal).ToList()
            };

        private static RegisteredChild CreateInternal(ChildInformationCreateModel model) =>
            new RegisteredChild
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth.Date,
                ShirtSize = model.ShirtSize
            };

        private static RegistrationReadModel CreateInternal(RegisteredPerson entity) =>
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

        private static ChildInformationReadModel CreateInternal(RegisteredChild entity) =>
            new ChildInformationReadModel
            {
                ParentId = entity.RegisteredPersonId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                ShirtSize = entity.ShirtSize
            };

        private static StripeChargeModel CreateStripeCharge(RegistrationCreateModel model) =>
            new StripeChargeModel
            {
                Email = model.EmailAddress,
                Token = model.StripeToken,
                Description = "Registration Fee",
                Amount = model.Children.Count > 1 ? 3000 : 2000
            };

        private static PaymentDetails CreateChargeRecord(StripeChargeModel model, string stripeCustomerId) =>
            new PaymentDetails
            {
                PaymentTimestamp = DateTimeOffset.Now,
                StripeCustomerId = stripeCustomerId,
                Amount = model.Amount / 100.0
            };
    }
}