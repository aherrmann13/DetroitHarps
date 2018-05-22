namespace Business.Test
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Mapping;
    using Business.Models;
    using Repository.Entities;
    using Xunit;

    public class EventProfileTest
    {
        public EventProfileTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.AddProfile<EventProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        [Fact]
        public void EventModelCreateTest()
        {
            var model = new EventCreateModel
            {
                Date = DateTimeOffset.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var entity = Mapper.Map<Event>(model);

            Assert.Equal(model.Date, entity.Date);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.Description, entity.Description);
        }

        [Fact]
        public void EventModelUpdateTest()
        {
            var model = new EventUpdateModel
            {
                Id = 1,
                Date = DateTimeOffset.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var entity = Mapper.Map<Event>(model);

            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Date, entity.Date);
            Assert.Equal(model.Title, entity.Title);
            Assert.Equal(model.Description, entity.Description);
        }

        [Fact]
        public void EventModelReadTest()
        {
            var entity = new Event
            {
                Id = 2,
                Date = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var model = Mapper.Map<EventReadModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Date, model.Date);
            Assert.Equal(entity.Title, model.Title);
            Assert.Equal(entity.Description, model.Description);
        }
    }
}