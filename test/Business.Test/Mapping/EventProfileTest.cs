namespace DetroitHarps.Business.Test.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Entities;
    using DetroitHarps.Business.Schedule.Models;
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
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                CanRegister = true
            };

            var eventEntity = Mapper.Map<Event>(eventModel);

            Assert.Equal(eventModel.StartDate, eventEntity.StartDate);
            Assert.Equal(eventModel.EndDate, eventEntity.EndDate);
            Assert.Equal(eventModel.Title, eventEntity.Title);
            Assert.Equal(eventModel.Description, eventEntity.Description);
            Assert.Equal(eventModel.CanRegister, eventEntity.CanRegister);
        }

        [Fact]
        public void EventModelMapTest()
        {
            var eventModel = new EventModel
            {
                Id = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                CanRegister = true
            };

            var eventEntity = Mapper.Map<Event>(eventModel);

            Assert.Equal(eventModel.Id, eventEntity.Id);
            Assert.Equal(eventModel.StartDate, eventEntity.StartDate);
            Assert.Equal(eventModel.EndDate, eventEntity.EndDate);
            Assert.Equal(eventModel.Title, eventEntity.Title);
            Assert.Equal(eventModel.Description, eventEntity.Description);
            Assert.Equal(eventModel.CanRegister, eventEntity.CanRegister);
        }

        [Fact]
        public void EventMapTest()
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

            var eventModel = Mapper.Map<EventModel>(eventEntity);

            Assert.Equal(eventEntity.Id, eventModel.Id);
            Assert.Equal(eventEntity.StartDate, eventModel.StartDate);
            Assert.Equal(eventEntity.EndDate, eventModel.EndDate);
            Assert.Equal(eventEntity.Title, eventModel.Title);
            Assert.Equal(eventEntity.Description, eventModel.Description);
            Assert.Equal(eventEntity.CanRegister, eventModel.CanRegister);
        }
    }
}