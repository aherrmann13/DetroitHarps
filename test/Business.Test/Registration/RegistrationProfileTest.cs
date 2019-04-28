namespace DetroitHarps.Business.Test.Registration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.DataTypes;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.Business.Registration.Models;
    using DetroitHarps.Business.Schedule.Entities;
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
                        ShirtSize = Guid.NewGuid().ToString(),
                        Events = new List<RegisterChildEventModel>
                        {
                            new RegisterChildEventModel
                            {
                                Answer = Answer.Yes,
                                EventId = 2
                            }
                        }
                    },
                    new RegisterChildModel
                    {
                        FirstName = Guid.NewGuid().ToString(),
                        LastName = Guid.NewGuid().ToString(),
                        Gender = Gender.Female,
                        DateOfBirth = DateTimeOffset.Now,
                        ShirtSize = Guid.NewGuid().ToString(),
                        Events = new List<RegisterChildEventModel>
                        {
                            new RegisterChildEventModel
                            {
                                Answer = Answer.No,
                                EventId = 2
                            },
                            new RegisterChildEventModel
                            {
                                Answer = Answer.Maybe,
                                EventId = 2
                            }
                        }
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
            Assert.Equal(registerModel.Children[0].Events.Count, registration.Children[0].Events.Count);
            Assert.Equal(registerModel.Children[0].Events[0].Answer, registration.Children[0].Events[0].Answer);
            Assert.Equal(registerModel.Children[0].Events[0].EventId, registration.Children[0].Events[0].EventId);
            Assert.Null(registration.Children[0].Events[0].EventSnapshot);

            Assert.Equal(registerModel.Children[1].FirstName, registration.Children[1].FirstName);
            Assert.Equal(registerModel.Children[1].LastName, registration.Children[1].LastName);
            Assert.Equal(registerModel.Children[1].Gender, registration.Children[1].Gender);
            Assert.Equal(registerModel.Children[1].DateOfBirth.Date, registration.Children[1].DateOfBirth.Date);
            Assert.Equal(registerModel.Children[1].ShirtSize, registration.Children[1].ShirtSize);
            Assert.Equal(registerModel.Children[1].Events.Count, registration.Children[1].Events.Count);
            Assert.Equal(registerModel.Children[1].Events[0].Answer, registration.Children[1].Events[0].Answer);
            Assert.Equal(registerModel.Children[1].Events[0].EventId, registration.Children[1].Events[0].EventId);
            Assert.Null(registration.Children[1].Events[0].EventSnapshot);
            Assert.Equal(registerModel.Children[1].Events[1].Answer, registration.Children[1].Events[1].Answer);
            Assert.Equal(registerModel.Children[1].Events[1].EventId, registration.Children[1].Events[1].EventId);
            Assert.Null(registration.Children[1].Events[1].EventSnapshot);

            Assert.Equal(default(double), registration.PaymentInformation.Amount);
            Assert.Equal(
                default(DateTimeOffset),
                registration.PaymentInformation.PaymentTimestamp);
            Assert.Equal(
                default(RegistrationPaymentType),
                registration.PaymentInformation.PaymentType);
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
                ShirtSize = Guid.NewGuid().ToString(),
                Events = new List<RegistrationChildEvent>
                {
                    new RegistrationChildEvent
                    {
                        Answer = Answer.Yes,
                        EventId = 1,
                        EventSnapshot = new RegistrationChildEventSnapshot()
                    },
                    new RegistrationChildEvent
                    {
                        Answer = Answer.No,
                        EventId = 2,
                        EventSnapshot = new RegistrationChildEventSnapshot()
                    }
                }
            };

            var registeredChildModel = Mapper.Map<RegisteredChildModel>(registrationChild);

            Assert.Equal(registrationChild.FirstName, registeredChildModel.FirstName);
            Assert.Equal(registrationChild.LastName, registeredChildModel.LastName);
            Assert.Equal(registrationChild.Gender, registeredChildModel.Gender);
            Assert.Equal(registrationChild.DateOfBirth.Date, registeredChildModel.DateOfBirth.Date);
            Assert.Equal(registrationChild.ShirtSize, registeredChildModel.ShirtSize);
            Assert.Null(registeredChildModel.EmailAddress);
            Assert.Equal(registrationChild.Events.Count, registeredChildModel.Events.Count);
            Assert.Equal(registrationChild.Events[0].Answer, registeredChildModel.Events[0].Answer);
            Assert.Equal(registrationChild.Events[0].EventId, registeredChildModel.Events[0].EventId);
            Assert.Equal(registrationChild.Events[1].Answer, registeredChildModel.Events[1].Answer);
            Assert.Equal(registrationChild.Events[1].EventId, registeredChildModel.Events[1].EventId);
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
                        ShirtSize = Guid.NewGuid().ToString(),
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

        [Fact]
        public void EventToEventSnapshotMapTest()
        {
            var eventEntity = new Event
            {
                Id = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                CanRegister = true
            };

            var eventSnapshotEntity = Mapper.Map<RegistrationChildEventSnapshot>(eventEntity);

            Assert.Equal(eventEntity.StartDate, eventSnapshotEntity.StartDate);
            Assert.Equal(eventEntity.EndDate, eventSnapshotEntity.EndDate);
            Assert.Equal(eventEntity.Title, eventSnapshotEntity.Title);
            Assert.Equal(eventEntity.Description, eventSnapshotEntity.Description);
            Assert.Equal(eventEntity.CanRegister, eventSnapshotEntity.CanRegister);
        }
    }
}