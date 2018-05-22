namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Business.Mapping;
    using Business.Models;
    using Repository.Entities;
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
        public void RegistrationModelCreateTest()
        {
            var model = new RegistrationCreateModel
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                PhoneNumber = Guid.NewGuid().ToString(),
                Address = Guid.NewGuid().ToString(),
                Address2 = Guid.NewGuid().ToString(),
                City = Guid.NewGuid().ToString(),
                State = Guid.NewGuid().ToString(),
                Zip = Guid.NewGuid().ToString(),
                Children = new List<ChildInformationCreateModel>
                {
                    new ChildInformationCreateModel(),
                    new ChildInformationCreateModel()
                },
                Comments = Guid.NewGuid().ToString(),
                RegistrationType = RegistrationType.Other
            };

            var entity = Mapper.Map<RegisteredPerson>(model);

            Assert.Equal(model.FirstName, entity.FirstName);
            Assert.Equal(model.LastName, entity.LastName);
            Assert.Equal(model.Email, entity.Email);
            Assert.Equal(model.PhoneNumber, entity.PhoneNumber);
            Assert.Equal(model.Address, entity.Address);
            Assert.Equal(model.Address2, entity.Address2);
            Assert.Equal(model.City, entity.City);
            Assert.Equal(model.State, entity.State);
            Assert.Equal(model.Zip, entity.Zip);
            Assert.Equal(model.Children.Count, entity.Children.Count);
        }

        [Fact]
        public void ChildInformationModelCreateTest()
        {
            var model = new ChildInformationCreateModel
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Gender = Guid.NewGuid().ToString(),
                DateOfBirth = DateTimeOffset.Now,
                ShirtSize = Guid.NewGuid().ToString(),
            };

            var entity = Mapper.Map<RegisteredChild>(model);

            Assert.Equal(model.FirstName, entity.FirstName);
            Assert.Equal(model.LastName, entity.LastName);
            Assert.Equal(model.Gender, entity.Gender);
            Assert.Equal(model.DateOfBirth, entity.DateOfBirth);
            Assert.Equal(model.ShirtSize, entity.ShirtSize);
        }

        [Fact]
        public void RegistrationModelReadTest()
        {
            var entity = new RegisteredPerson
            {
                Id = 2,
                SeasonId = 2,
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                PhoneNumber = Guid.NewGuid().ToString(),
                Address = Guid.NewGuid().ToString(),
                Address2 = Guid.NewGuid().ToString(),
                City = Guid.NewGuid().ToString(),
                State = Guid.NewGuid().ToString(),
                Zip = Guid.NewGuid().ToString(),
                Children = new List<RegisteredChild>
                {
                    new RegisteredChild(),
                    new RegisteredChild()
                },
                PaymentDetails = new List<PaymentDetails>
                {
                    new PaymentDetails(),
                    new PaymentDetails()
                }
            };

            var model = Mapper.Map<RegistrationReadModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.FirstName, model.FirstName);
            Assert.Equal(entity.LastName, model.LastName);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.PhoneNumber, model.PhoneNumber);
            Assert.Equal(entity.Address, model.Address);
            Assert.Equal(entity.Address2, model.Address2);
            Assert.Equal(entity.City, model.City);
            Assert.Equal(entity.State, model.State);
            Assert.Equal(entity.Zip, model.Zip);
            Assert.Equal(entity.PaymentDetails.Any(), model.HasPaid);
            Assert.Equal(entity.RegistrationTimestamp, model.RegistrationTimestamp);
        }

        [Fact]
        public void RegistrationModelReadHasPaidMappedOnNoPaymentDetailsTest()
        {
            var entity = new RegisteredPerson();

            var model = Mapper.Map<RegistrationReadModel>(entity);

            Assert.False(model.HasPaid);
        }

        [Fact]
        public void RegistrationModelReadHasPaidMappedWithPaymentDetailsTest()
        {
            var entity = new RegisteredPerson
            {
                PaymentDetails = new List<PaymentDetails> { new PaymentDetails() }
            };

            var model = Mapper.Map<RegistrationReadModel>(entity);

            Assert.True(model.HasPaid);
        }

        [Fact]
        public void ChildInformationModelReadTest()
        {
            var model = new RegisteredChild
            {
                Id = 2,
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Gender = Guid.NewGuid().ToString(),
                DateOfBirth = DateTimeOffset.Now.Date,
                ShirtSize = Guid.NewGuid().ToString(),
            };

            var entity = Mapper.Map<ChildInformationReadModel>(model);

            Assert.Equal(model.Id, model.Id);
            Assert.Equal(model.FirstName, model.FirstName);
            Assert.Equal(model.LastName, model.LastName);
            Assert.Equal(model.Gender, model.Gender);
            Assert.Equal(entity.DateOfBirth, model.DateOfBirth);
            Assert.Equal(entity.ShirtSize, model.ShirtSize);
        }
    }
}