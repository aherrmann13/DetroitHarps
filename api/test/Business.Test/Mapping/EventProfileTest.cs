namespace Business.Test.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Mapping;
    using Business.Models;
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
        public void EventCreateModelMapTest()
        {
            var eventModel = new EventCreateModel
            {
                Date = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var eventEntity = Mapper.Map<Event>(eventModel);

            Assert.Equal(eventModel.Date, eventEntity.Date);
            Assert.Equal(eventModel.Title, eventEntity.Title);
            Assert.Equal(eventModel.Description, eventEntity.Description);
        }

        [Fact]
        public void EventModelMapTest()
        {
            var eventModel = new EventModel
            {
                Id = 1,
                Date = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var eventEntity = Mapper.Map<Event>(eventModel);

            Assert.Equal(eventModel.Id, eventEntity.Id);
            Assert.Equal(eventModel.Date, eventEntity.Date);
            Assert.Equal(eventModel.Title, eventEntity.Title);
            Assert.Equal(eventModel.Description, eventEntity.Description);
        }

        [Fact]
        public void EventMapTest()
        {
            var eventEntity = new Event
            {
                Id = 1,
                Date = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };

            var eventModel = Mapper.Map<EventModel>(eventEntity);

            Assert.Equal(eventEntity.Id, eventModel.Id);
            Assert.Equal(eventEntity.Date, eventModel.Date);
            Assert.Equal(eventEntity.Title, eventModel.Title);
            Assert.Equal(eventEntity.Description, eventModel.Description);
        }
    } 
}