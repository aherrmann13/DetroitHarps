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
            var createModels = GetValidModels().ToArray();

            var response = _manager.Register(createModels).ToList();

            var entities = DbContext.Set<RegisteredPerson>()
                .Include(x => x.Children)
                .Include(x => x.PaymentDetails)
                .Where(x => x.Season.Year.Equals(DateTime.Now.Year))
                .AsNoTracking()
                .ToList();

            AssertCreated(createModels, entities);
            AssertResponseCorrect(response, entities);
        }

                [Fact]
        public void CreateNullInputTest()
        {
            var response = _manager.Register((RegistrationCreateModel[])null);

            Assert.Empty(response);
        }

        [Fact]
        public void CreateEmptyInputTest()
        {
            var response = _manager.Register(new RegistrationCreateModel[0]);

            Assert.Empty(response);
        }

        [Fact]
        public void CreateNullInInputTest()
        {
            var createModels = GetValidModels().ToList();
            createModels.Add((RegistrationCreateModel)null);

            var response = _manager.Register(createModels.ToArray()).ToList();

            var entities = DbContext.Set<RegisteredPerson>()
                .Include(x => x.Children)
                .Include(x => x.PaymentDetails)
                .Where(x => x.Season.Year.Equals(DateTime.Now.Year))
                .AsNoTracking()
                .ToList();

            Assert.Equal(createModels.Count() - 1, response.Count());
            AssertCreated(createModels.Where(x => x != null).ToList(), entities);
            AssertResponseCorrect(response, entities);
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

        private IEnumerable<RegistrationCreateModel> GetValidModels()
        {
            for(var i = 0; i < 5; i ++)
            {
                yield return new RegistrationCreateModel
                {
                    FirstName = $"firstname{i}",
                    LastName = $"lastname{i}",
                    EmailAddress = $"emailaddress{i}",
                    PhoneNumber = $"phonenumber{i}",
                    Address = $"address{i}",
                    Address2 = $"address2{i}",
                    City = $"city{i}",
                    State = $"state{i}",
                    Zip = $"zip{i}",
                    Children = 
                    {
                        new ChildInformationCreateModel
                        {
                            FirstName = $"child1-firstname{i}",
                            LastName = $"child1-lastname{i}",
                            DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                            ShirtSize = $"child1-shirtsize{i}",
                        },
                        new ChildInformationCreateModel
                        {
                            FirstName = $"child2-firstname{i}",
                            LastName = $"child2-lastname{i}",
                            DateOfBirth = DateTimeOffset.Now.AddYears(-10),
                            ShirtSize = $"child2-shirtsize{i}",
                        }
                    }
                };
            }
        }

        private static void AssertCreated(
            IEnumerable<RegistrationCreateModel> models,
            IEnumerable<RegisteredPerson> entities)
        {
            Assert.Equal(models.Count(), entities.Count()); 

            var editableEntityList = entities.ToList();
            // TODO : ForEach on IList, IEnumerable in tools
            foreach(var model in models)
            {
                var entity = editableEntityList.FirstOrDefault(x => IsEqual(model, x));

                Assert.NotNull(entity);
                Assert.Empty(entity.PaymentDetails);
                AssertEqual(model.Children, entity.Children);

                editableEntityList.Remove(entity);
            }
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
                PaymentDetails = paymentDetails
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