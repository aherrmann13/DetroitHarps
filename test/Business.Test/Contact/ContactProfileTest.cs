namespace DetroitHarps.Business.Test.Contact
{
    using System;
    using AutoMapper;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.Business.Contact.Models;
    using Xunit;

    public class ContactProfileTest
    {
        public ContactProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<ContactProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void MessageModelToMessageMapTest()
        {
            var model = new MessageModel
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            var beforeMap = DateTimeOffset.Now;
            var entity = Mapper.Map<Message>(model);

            Assert.NotEqual(default(Guid), entity.Id);
            Assert.InRange(entity.Timestamp, beforeMap, DateTimeOffset.Now);
            Assert.Equal(model.FirstName, entity.FirstName);
            Assert.Equal(model.LastName, entity.LastName);
            Assert.Equal(model.Email, entity.Email);
            Assert.Equal(model.Body, entity.Body);
        }

        [Fact]
        public void MessageTimestampMappingNotStaticValueTest()
        {
            var model = new MessageModel
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            var entity1 = Mapper.Map<Message>(model);
            var entity2 = Mapper.Map<Message>(model);

            Assert.NotEqual(entity1.Timestamp, entity2.Timestamp);
        }

        [Fact]
        public void MessageToMessageReadModelMapTest()
        {
            var entity = new Message
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Body = Guid.NewGuid().ToString()
            };

            var model = Mapper.Map<MessageReadModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Timestamp, model.Timestamp);
            Assert.True(model.IsRead);
            Assert.Equal(entity.FirstName, model.FirstName);
            Assert.Equal(entity.LastName, model.LastName);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.Body, model.Body);
        }
    }
}