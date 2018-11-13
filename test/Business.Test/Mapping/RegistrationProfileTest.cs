namespace DetroitHarps.Business.Test.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.DataTypes;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using Xunit;

    public class RegistrationProfileTest
    {
        public RegistrationProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<RegistrationProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void RegistrationMapTest()
        {
            var registerModel = new RegisterModel
            {
                ContactInformation = new RegisterContactInformationModel
                {
                    Email = Guid.NewGuid().ToString(),
                    PhoneNumber = Guid.NewGuid().ToString(),
                    Address = Guid.NewGuid().ToString(),
                    Address2 = Guid.NewGuid().ToString(),
                    City = Guid.NewGuid().ToString(),
                    State = Guid.NewGuid().ToString(),
                    Zip = Guid.NewGuid().ToString()
                },
                Parent = new RegisterParentModel
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                },
                Children =
                {
                    new RegisterChildModel
                    {
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        Gender = Gender.Female,
                        DateOfBirth = DateTimeOffset.Now,
                        ShirtSize = Guid.NewGuid().ToString()
                    },
                    new RegisterChildModel
                    {
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        Gender = Gender.Female,
                        DateOfBirth = DateTimeOffset.Now,
                        ShirtSize = Guid.NewGuid().ToString()
                    }
                }
            };

            var registration = Mapper.Map<Registration>(registerModel);

            Assert.Equal(DateTimeOffset.Now.Year, registration.SeasonYear);
            Assert.Equal(registerModel.Parent.FirstName, registration.Parent.FirstName);
            Assert.Equal(registerModel.Parent.LastName, registration.Parent.LastName);

            Assert.Equal(registerModel.ContactInformation.Email, registration.ContactInformation.Email);
            Assert.Equal(registerModel.ContactInformation.PhoneNumber, registration.ContactInformation.PhoneNumber);
            Assert.Equal(registerModel.ContactInformation.Address, registration.ContactInformation.Address);
            Assert.Equal(registerModel.ContactInformation.Address2, registration.ContactInformation.Address2);
            Assert.Equal(registerModel.ContactInformation.City, registration.ContactInformation.City);
            Assert.Equal(registerModel.ContactInformation.State, registration.ContactInformation.State);
            Assert.Equal(registerModel.ContactInformation.Zip, registration.ContactInformation.Zip);

            Assert.Equal(registerModel.Children[0].FirstName, registration.Children[0].FirstName);
            Assert.Equal(registerModel.Children[0].LastName, registration.Children[0].LastName);
            Assert.Equal(registerModel.Children[0].Gender, registration.Children[0].Gender);
            Assert.Equal(registerModel.Children[0].DateOfBirth.Date, registration.Children[0].DateOfBirth.Date);
            Assert.Equal(registerModel.Children[0].ShirtSize, registration.Children[0].ShirtSize);

            Assert.Equal(registerModel.Children[1].FirstName, registration.Children[1].FirstName);
            Assert.Equal(registerModel.Children[1].LastName, registration.Children[1].LastName);
            Assert.Equal(registerModel.Children[1].Gender, registration.Children[1].Gender);
            Assert.Equal(registerModel.Children[1].DateOfBirth.Date, registration.Children[1].DateOfBirth.Date);
            Assert.Equal(registerModel.Children[1].ShirtSize, registration.Children[1].ShirtSize);

            Assert.Null(registration.PaymentInformation);
        }

        [Fact]
        public void RegisteredParentModelMapTest()
        {
            var registration = new Registration
            {
                Id = 5,
                Parent = new RegistrationParent
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                },
                ContactInformation = new RegistrationContactInformation
                {
                    Email = Guid.NewGuid().ToString()
                },
                Children = { new RegistrationChild(), new RegistrationChild() }
            };

            var registeredParentModel = Mapper.Map<RegisteredParentModel>(registration);

            Assert.Equal(registration.Id, registeredParentModel.RegistrationId);
            Assert.Equal(registration.Parent.FirstName, registeredParentModel.FirstName);
            Assert.Equal(registration.Parent.LastName, registeredParentModel.LastName);
            Assert.Equal(registration.ContactInformation.Email, registeredParentModel.Email);
            Assert.Equal(registration.Children.Count, registeredParentModel.ChildCount);
        }

        [Fact]
        public void RegisteredChildModelMapTest()
        {
            var registrationChild = new RegistrationChild
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Gender = Gender.Female,
                DateOfBirth = DateTime.Now,
                ShirtSize = Guid.NewGuid().ToString()
            };

            var registeredChildModel = Mapper.Map<RegisteredChildModel>(registrationChild);

            Assert.Equal(registrationChild.FirstName, registeredChildModel.FirstName);
            Assert.Equal(registrationChild.LastName, registeredChildModel.LastName);
            Assert.Equal(registrationChild.Gender, registeredChildModel.Gender);
            Assert.Equal(registrationChild.DateOfBirth.Date, registeredChildModel.DateOfBirth.Date);
            Assert.Equal(registrationChild.ShirtSize, registeredChildModel.ShirtSize);
            Assert.Null(registeredChildModel.EmailAddress);
        }

        [Fact]
        public void RegisteredChildModelListMapTest()
        {
            var registration = new Registration
            {
                Parent = new RegistrationParent
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                },
                ContactInformation = new RegistrationContactInformation
                {
                    Email = Guid.NewGuid().ToString()
                },
                Children = new List<RegistrationChild>
                {
                    new RegistrationChild
                    {
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        Gender = Gender.Female,
                        DateOfBirth = DateTime.Now,
                        ShirtSize = Guid.NewGuid().ToString()
                    },
                    new RegistrationChild
                    {
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        Gender = Gender.Female,
                        DateOfBirth = DateTime.Now,
                        ShirtSize = Guid.NewGuid().ToString()
                    }
                }
            };

            var registeredChildren = Mapper.Map<IEnumerable<RegisteredChildModel>>(registration);

            Assert.Collection(
            registeredChildren,
            x =>
            {
                Assert.Equal(x.FirstName, registration.Children[0].FirstName);
                Assert.Equal(x.LastName, registration.Children[0].LastName);
                Assert.Equal(x.Gender, registration.Children[0].Gender);
                Assert.Equal(x.DateOfBirth.Date, registration.Children[0].DateOfBirth.Date);
                Assert.Equal(x.ShirtSize, registration.Children[0].ShirtSize);
                Assert.Equal(x.EmailAddress, registration.ContactInformation.Email);
                Assert.Equal(x.ParentFirstName, registration.Parent.FirstName);
                Assert.Equal(x.ParentLastName, registration.Parent.LastName);
            },
            x =>
            {
                Assert.Equal(x.FirstName, registration.Children[1].FirstName);
                Assert.Equal(x.LastName, registration.Children[1].LastName);
                Assert.Equal(x.Gender, registration.Children[1].Gender);
                Assert.Equal(x.DateOfBirth.Date, registration.Children[1].DateOfBirth.Date);
                Assert.Equal(x.ShirtSize, registration.Children[1].ShirtSize);
                Assert.Equal(x.EmailAddress, registration.ContactInformation.Email);
                Assert.Equal(x.ParentFirstName, registration.Parent.FirstName);
                Assert.Equal(x.ParentLastName, registration.Parent.LastName);
            });
        }
    }
}