namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Repository;
    using Repository.Entities;
    using Tools;
    using Xunit;

    public class RegistrationManagerTest : ManagerTestBase
    {
        private readonly IRegistrationManager _manager;

        public RegistrationManagerTest() : base()
        {
            _manager = ServiceProvider.GetRequiredService<IRegistrationManager>();

            SeedSeason();
        }

        [Fact]
        public void CreateSuccessTest()
        {
            var createModel = GetValidModel();

            var response = _manager.Register(createModel);

            var entity = DbContext.Set<RegisteredPerson>()
                .Include(x => x.Children)
                .Include(x => x.PaymentDetails)
                .AsNoTracking()
                .First(x => x.Season.Year.Equals(DateTime.Now.Year));

            AssertEqual(createModel, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void CreateSuccessDifferentPaymentTypeTest()
        {
            var createModel = GetValidModel();

            createModel.RegistrationType = RegistrationType.Paypal;

            var response = _manager.Register(createModel);

            var entity = DbContext.Set<RegisteredPerson>()
                .Include(x => x.Children)
                .Include(x => x.PaymentDetails)
                .AsNoTracking()
                .First(x => x.Season.Year.Equals(DateTime.Now.Year));

            AssertEqual(createModel, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void CreateSuccessEmailsSentTest()
        {
            var createModel = GetValidModel();
            var subject1 = string.Empty;
            var subject2 = string.Empty;
            var body1 = string.Empty;
            var body2 = string.Empty;
            var index = 0;
            ContactManagerMock
                .Setup(x => x.Contact(It.IsAny<string>(), It.IsAny<string>()))
                .Callback<string, string>((x, y) => {
                    if(index == 0)
                    {
                        subject1 = x;
                        body1 = y;
                        index ++;
                    }
                    else if(index == 1)
                    {
                        subject2 = x;
                        body2 = y;
                    }
                    else
                    {
                        throw new InvalidOperationException("Method called too many times");
                    }
                });

            var response = _manager.Register(createModel);
            var subject1String = $"New Registration! {createModel.FirstName} {createModel.LastName}";
            var subject2String = $"Registration comments from {createModel.FirstName} {createModel.LastName}";

            var generatedBody1 = GenerateRegistrationEmailBodyForParent(createModel) + GenerateRegistrationEmailBodyForChildren(createModel.Children);

            Assert.Equal(subject1String, subject1);
            Assert.Equal(generatedBody1, body1);
            Assert.Equal(subject2String, subject2);
            Assert.Equal(createModel.Comments, body2);
            
            ContactManagerMock.Verify(x => 
                x.Contact(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Exactly(2));
        }

        [Fact]
        public void CreateSuccessNoCommentEmailSentTest()
        {
            var createModel = GetValidModel();
            createModel.Comments = string.Empty;

            var response = _manager.Register(createModel);

            ContactManagerMock.Verify(x => 
                x.Contact(It.IsAny<string>(), It.IsAny<string>()),
                Times.Once());
        }

        [Fact]
        public void CreateNullInputThrowsExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Register((RegistrationCreateModel)null));
        }

        [Fact]
        public void GetAllPersonsTest()
        {
            var entities = SeedRegistration();

            var response = _manager.GetAll();

            AssertEqual(response, entities);
        }

        [Fact]
        public void GetAllChildInformationSuccessTest()
        {
            var entities = SeedRegistration();

            var response = _manager.GetAllChildren();

            AssertEqual(response, entities.SelectMany(x => x.Children));
        }

        private RegistrationCreateModel GetValidModel() =>
            new RegistrationCreateModel
            {
                FirstName = "firstname",
                LastName = "lastname",
                EmailAddress = "emailaddress",
                PhoneNumber = "phonenumber",
                Address = "address",
                Address2 = "address2",
                City = "city",
                State = "state",
                Zip = "zip",
                RegistrationType = RegistrationType.Cash,
                Comments = "comments",
                Children = 
                {
                    new ChildInformationCreateModel
                    {
                        FirstName = "child1-firstname",
                        LastName = "child1-lastname",
                        Gender = "male",
                        DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                        ShirtSize = "child1-shirtsize",
                    },
                    new ChildInformationCreateModel
                    {
                        FirstName = "child2-firstname",
                        LastName = "child2-lastname",
                        Gender = "male",
                        DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                        ShirtSize = "child2-shirtsize",
                    }
                }
            };

        private static void AssertEqual(RegistrationCreateModel model, RegisteredPerson entity)
        {
            Assert.True(IsEqual(model, entity));

            Assert.Single(entity.PaymentDetails);
            var payment = entity.PaymentDetails.First();
            Assert.Equal(payment.Amount, model.Children.Count > 1 ? 30.0 : 20.0);
            Assert.Equal(model.RegistrationType.ToString(), payment.PaymentType);
            Assert.False(payment.VerfiedPayment);
            Assert.InRange(entity.RegistrationTimestamp, DateTimeOffset.Now.AddMinutes(-1), DateTimeOffset.Now);

            AssertEqual(model.Children, entity.Children);
        }

        private static void AssertEqual(
            IEnumerable<ChildInformationCreateModel> models,
            IEnumerable<RegisteredChild> entities)
        {
            Assert.Equal(models.Count(), entities.Count()); 

            var editableEntityList = entities.ToList();
            // TODO : ForEach on IList, IEnumerable in tools
            foreach(var model in models)
            {
                var entity = editableEntityList.FirstOrDefault(x => IsEqual(model, x));

                Assert.NotNull(entity);

                editableEntityList.Remove(entity);
            }
        }

        private static void AssertEqual(
            IEnumerable<ChildInformationReadModel> models,
            IEnumerable<RegisteredChild> entities)
        {
            Assert.Equal(models.Count(), entities.Count()); 

            var editableEntityList = entities.ToList();
            // TODO : ForEach on IList, IEnumerable in tools
            foreach(var model in models)
            {
                var entity = editableEntityList.FirstOrDefault(x => 
                    model.ParentId.Equals(x.RegisteredPersonId) && IsEqual(model, x));

                Assert.NotNull(entity);

                editableEntityList.Remove(entity);
            }
        }

        private static void AssertEqual(
            IEnumerable<RegistrationReadModel> models,
            IEnumerable<RegisteredPerson> entities)
        {
            Assert.Equal(models.Count(), entities.Count()); 

            var editableEntityList = entities.ToList();
            // TODO : ForEach on IList, IEnumerable in tools
            foreach(var model in models)
            {
                var entity = editableEntityList.FirstOrDefault(x => x.Id.Equals(model.Id));

                Assert.NotNull(entity);
                Assert.True(IsEqual(model, entity));
                Assert.True(model.HasPaid == entity.PaymentDetails.Any());
                Assert.Equal(model.RegistrationTimestamp, entity.RegistrationTimestamp);

                editableEntityList.Remove(entity);
            }
        }

        private static void AssertResponseCorrect(
            IList<int> response,
            IList<RegisteredPerson> entities)
        {
            Assert.Equal(response.Count, entities.Count);

            Assert.Equal(response.OrderBy(x => x), entities.Select(x => x.Id).OrderBy(x => x));
        }
        
        private static bool IsEqual(RegistrationModelBase model, RegisteredPerson entity) =>
            model.FirstName.EqualOrdinal(entity.FirstName) &&
            model.LastName.EqualOrdinal(entity.LastName) &&
            model.EmailAddress.EqualOrdinal(entity.Email) &&
            model.PhoneNumber.EqualOrdinal(entity.PhoneNumber) &&
            model.Address.EqualOrdinal(entity.Address) &&
            model.Address2.EqualOrdinal(entity.Address2) &&
            model.City.EqualOrdinal(entity.City) &&
            model.State.EqualOrdinal(entity.State) &&
            model.Zip.EqualOrdinal(entity.Zip);

        private static bool IsEqual(ChildInformationModelBase model, RegisteredChild entity) =>
            model.FirstName.EqualOrdinal(entity.FirstName) &&
            model.LastName.EqualOrdinal(entity.LastName) &&
            model.Gender.EqualOrdinal(entity.Gender) &&
            model.DateOfBirth.Date.Equals(entity.DateOfBirth.Date) &&
            model.ShirtSize.EqualOrdinal(entity.ShirtSize);

        private static RegisteredPerson GetValidPerson(int seasonId, int childCount, bool hasPaid){
            
            var children = new List<RegisteredChild>();
            for(int c = 0; c < childCount; c++)
            {
                children.Add(GetValidChild());
            }

            var paymentDetails = new List<PaymentDetails>();
            if(hasPaid)
            {
                paymentDetails.Add(GetValidPaymentDetails());
            }

            return new RegisteredPerson
            {
                SeasonId = seasonId,
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                PhoneNumber = Guid.NewGuid().ToString(),
                Address = Guid.NewGuid().ToString(),
                Address2 = Guid.NewGuid().ToString(),
                City = Guid.NewGuid().ToString(),
                State = Guid.NewGuid().ToString(),
                Zip = Guid.NewGuid().ToString(),
                Children = children,
                PaymentDetails = paymentDetails,
                RegistrationTimestamp = DateTimeOffset.Now
            };
        }

        private static RegisteredChild GetValidChild() =>
            new RegisteredChild
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                DateOfBirth = DateTime.Now.AddYears(-10),
                ShirtSize = Guid.NewGuid().ToString()
            };

        private static PaymentDetails GetValidPaymentDetails() =>
            new PaymentDetails
            {
                Amount = 20,
                PaymentTimestamp = DateTimeOffset.Now
            };

        private static string GenerateRegistrationEmailBodyForParent(RegistrationCreateModel model) =>
            $"First Name: {model.FirstName}{Environment.NewLine}"+
            $"Last Name: {model.LastName}{Environment.NewLine}"+
            $"EmailAddress: {model.EmailAddress}{Environment.NewLine}"+
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

        private void SeedSeason()
        {
            var season = new Season
            {
                Year = DateTime.Now.Year
            };
            
            DbContext.Add(season);
            DbContext.SaveChanges();
        }

        private IEnumerable<RegisteredPerson> SeedRegistration()
        {
            var seasonId = DbContext.Set<Season>().AsNoTracking().First().Id;
            var persons = new List<RegisteredPerson>
            {
                GetValidPerson(seasonId, 2, true),
                GetValidPerson(seasonId, 3, false),
                GetValidPerson(seasonId, 1, false),
                GetValidPerson(seasonId, 3, true),
                GetValidPerson(seasonId, 1, true),        
            };

            DbContext.AddRange(persons);
            DbContext.SaveChanges();
            return persons;
        }
    }
}