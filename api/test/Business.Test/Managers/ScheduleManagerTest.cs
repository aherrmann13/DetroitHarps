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

    public class ScheduleManagerTest : ManagerTestBase
    {
        private readonly IScheduleManager _manager;

        public ScheduleManagerTest() : base()
        {
            _manager = ServiceProvider.GetRequiredService<IScheduleManager>();
        }

        [Fact]
        public void CreateSuccessTest()
        {
            var model = GetValidCreateModel();

            var response = _manager.Create(model);

            var entity = DbContext.Set<Event>()
                .AsNoTracking()
                .First();

            AssertEqual(model, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void CreateNullInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Create(null));
        }

        [Fact]
        public void UpdateSuccessTest()
        {
            var entities = SeedEvents();

            var model = GetValidUpdateModel();

            var response = _manager.Update(model);

            var entity = DbContext.Set<Event>()
                .AsNoTracking()
                .First(x => x.Id.Equals(model.Id));

            AssertEqual(model, entity);
            Assert.Equal(response, entity.Id);
        }

        [Fact]
        public void UpdateIdDoesntExistExceptionTest()
        {
            var entities = SeedEvents();

            var model = GetValidUpdateModel();
            model.Id = GetNonExistantId<Event>();

            Assert.Throws<InvalidOperationException>(() => _manager.Update(model));
        }

        [Fact]
        public void UpdateNullModelTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.Update(null));
        }

        [Fact]
        public void DeleteSuccess()
        {
            var seededEntities = SeedEvents().ToList();

            var id = seededEntities.First().Id;

            var response = _manager.Delete(id);

            var entities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(seededEntities.Count - 1, entities.Count);
            Assert.Null(entities.FirstOrDefault(x => x.Id.Equals(id)));
            Assert.Equal(response, id);
        }

        [Fact]
        public void DeleteIdDoesntExistExceptionTest()
        {
            var id = GetNonExistantId<Event>();
            Assert.Throws<InvalidOperationException>(() => _manager.Delete(id));
        }

        [Fact]
        public void GetAllSuccessTest()
        {
            var entities = SeedEvents();

            var response = _manager.GetAll();

            AssertEqual(response, entities);
        }

        [Fact]
        public void GetSuccessTest()
        {
            var entities = SeedEvents();
            var id = entities.Select(x => x.Id).First();

            var response = _manager.Get(id);

            var entity = DbContext.Set<Event>()
                .AsNoTracking()
                .First(x => x.Id.Equals(id));

            AssertEqual(response, entity);
        }

        [Fact]
        public void GetIdDoesntExistExceptionTest()
        {
            var id = GetNonExistantId<Event>();
            Assert.Throws<InvalidOperationException>(() => _manager.Get(id));
        }

        private static EventCreateModel GetValidCreateModel() =>
            new EventCreateModel
            {
                Date = DateTimeOffset.Now.AddDays(-2),
                Title = $"title",
                Description = $"description"
            };

        private EventUpdateModel GetValidUpdateModel()
        {
            var entity = DbContext.Set<Event>().AsNoTracking().First();

            return new EventUpdateModel
            {
                Id = entity.Id,
                Date = DateTimeOffset.Now.AddDays(-2),
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
        }
            

        private static void AssertEqual<T>(T model, Event entity)
            where T : EventModelBase
        {
            Assert.Equal(model.Date.Date, entity.Date.Date);
            Assert.Equal(model.Description, entity.Description);
            Assert.Equal(model.Title, entity.Title);
        }

        private static void AssertEqual(EventUpdateModel model, Event entity)
        {
            Assert.Equal(model.Id, entity.Id);
            AssertEqual<EventModelBase>(model, entity);
        }

        private static void AssertEqual(EventReadModel model, Event entity)
        {
            Assert.Equal(model.Id, entity.Id);
            AssertEqual<EventModelBase>(model, entity);
        }

        private static void AssertEqual(IEnumerable<EventReadModel> models, IEnumerable<Event> entities)
        {
            Assert.Equal(models.Count(), entities.Count());

            // TODO : ForEach on IEnumerable in tools
            foreach(var model in models)
            {
                var entity = entities.FirstOrDefault(x => x.Id.Equals(model.Id));

                Assert.NotNull(entity);

                Assert.True(IsEqual(model, entity));
            }
        }

        private static bool IsEqual(EventModelBase model, Event entity)
        {
            return model.Date.Date.Equals(entity.Date) && 
                    model.Title.EqualOrdinal(entity.Title) && 
                    model.Description.EqualOrdinal(entity.Description);
        }

        private IEnumerable<Event> SeedEvents()
        {
            var eventList = new List<Event>();
            for(var i = 0; i < 5; i ++)
            {
               var entity = new Event
               {
                    Date = DateTimeOffset.Now.AddDays(i * -1).Date,
                    Title = $"title{i}",
                    Description = $"description{i}"
               };

               eventList.Add(entity);
            }
            DbContext.AddRange(eventList);
            DbContext.SaveChanges();

            return eventList;
        }
    }
}
