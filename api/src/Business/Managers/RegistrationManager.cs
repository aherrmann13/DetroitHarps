namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
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
        private readonly IContactManager _contactManager;
        private int _currentYear;

        public RegistrationManager(ApiDbContext dbContext, IStripeManager stripeManager, IContactManager contactManager)
        {
            Guard.NotNull(dbContext, nameof(dbContext));
            Guard.NotNull(stripeManager, nameof(stripeManager));
            Guard.NotNull(contactManager, nameof(contactManager));

            _dbContext = dbContext;
            _stripeManager = stripeManager;
            _contactManager = contactManager;
            _currentYear = DateTime.Now.Year;
        }

        public int Register(RegistrationCreateModel model)
        {
            Guard.NotNull(model, nameof(model));

            var season = _dbContext.Set<Season>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Year.Equals(_currentYear));

            var entity = Mapper.Map<RegisteredPerson>(model);
            entity.SeasonId = season.Id;
            entity.PaymentDetails.Add(CreateUnverifiedPaymentDetails(model));
            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            SendRegistrationNotification(model);
            SendRegistrationComments($"{model.FirstName} {model.LastName}", model.Comments);

            return entity.Id;
        }

        public IEnumerable<RegistrationReadModel> GetAll() =>
            _dbContext.Set<RegisteredPerson>()
                .AsNoTracking()
                .Include(x => x.PaymentDetails)
                .Where(x => x.Season.Year.Equals(_currentYear))
                .Select(Mapper.Map<RegistrationReadModel>);

        public IEnumerable<ChildInformationReadModel> GetAllChildren() =>
            _dbContext.Set<RegisteredChild>()
                .AsNoTracking()
                .Where(x => x.RegisteredPerson.Season.Year.Equals(_currentYear))
                .Select(Mapper.Map<ChildInformationReadModel>);

        private static PaymentDetails CreateUnverifiedPaymentDetails(RegistrationCreateModel model) =>
            new PaymentDetails
            {
                PaymentTimestamp = DateTimeOffset.Now,
                VerfiedPayment = false,
                PaymentType = model.RegistrationType.ToString(),
                Amount = model.Children.Count > 1 ? 30 : 20
            };

        private static string GenerateRegistrationEmailBodyForParent(RegistrationCreateModel model) =>
            $"First Name: {model.FirstName}{Environment.NewLine}"+
            $"Last Name: {model.LastName}{Environment.NewLine}"+
            $"EmailAddress: {model.Email}{Environment.NewLine}"+
            $"PhoneNumber: {model.PhoneNumber}{Environment.NewLine}"+
            $"Address: {model.Address}{Environment.NewLine}"+
            $"Address2: {model.Address2}{Environment.NewLine}"+
            $"City: {model.City}{Environment.NewLine}"+
            $"State: {model.State}{Environment.NewLine}"+
            $"Zip: {model.Zip}{Environment.NewLine}{Environment.NewLine}";

        private static string GenerateRegistrationEmailBodyForChildren(IEnumerable<ChildInformationCreateModel> models)
        {
            var returnString = string.Empty;
            var childNumber = 0;
            foreach(var child in models)
            {
                childNumber ++;
                var childString = $"First Name: {child.FirstName}{Environment.NewLine}"+
                $"Last Name: {child.LastName}{Environment.NewLine}"+
                $"Gender: {child.Gender}{Environment.NewLine}"+
                $"DateOfBirth: {child.DateOfBirth}{Environment.NewLine}"+
                $"ShirtSize: {child.ShirtSize}{Environment.NewLine}";

                returnString += $"Child {childNumber}:{Environment.NewLine}";
                returnString += childString;
            }

            return returnString;
        }

        private void SendRegistrationNotification(RegistrationCreateModel model)
        {
            var subject = $"New Registration! {model.FirstName} {model.LastName}";
            
            var body = GenerateRegistrationEmailBodyForParent(model);
            body += GenerateRegistrationEmailBodyForChildren(model.Children);

            _contactManager.Contact(subject, body);
        }
    
        private void SendRegistrationComments(string from, string comments)
        {
            if(!string.IsNullOrWhiteSpace(comments))
            {
                _contactManager.Contact($"Registration comments from {from}", comments);
            }
        }
    }
}