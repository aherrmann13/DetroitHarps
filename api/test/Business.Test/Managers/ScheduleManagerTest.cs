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
            var createModels = GetValidModels().ToArray();

            var response = _manager.CreateEvent(createModels).ToList();

            var entities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            AssertCreated(createModels, entities);
            AssertResponseCorrect(response, entities);
        }

        [Fact]
        public void CreateNullInputTest()
        {
            var response = _manager.CreateEvent(null);

            Assert.Empty(response);
        }

        [Fact]
        public void CreateEmptyInputTest()
        {
            var response = _manager.CreateEvent(new EventCreateModel[0]);

            Assert.Empty(response);
        }

        [Fact]
        public void CreateNullInInputTest()
        {
            var createModels = GetValidModels().ToList();
            createModels.Add((EventCreateModel)null);

            var response = _manager.CreateEvent(createModels.ToArray()).ToList();

            var entities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(createModels.Count() - 1, response.Count());
            AssertCreated(createModels.Where(x => x != null).ToList(), entities);
            AssertResponseCorrect(response, entities);
        }

        [Fact]
        public void UpdateSuccessTest()
        {
            var entities = SeedEvents(DbContext);

            var updateModels = entities.Select(x => new EventUpdateModel
            {
                Id = x.Id,
                Date = DateTimeOffset.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            }).ToArray();

            var response = _manager.UpdateEvent(updateModels).ToList();

            var updatedEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            AssertUpdated(updateModels, updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        [Fact]
        public void UpdateNullInputTest()
        {
            var response = _manager.UpdateEvent(null);

            Assert.Empty(response);
        }

        [Fact]
        public void UpdateEmptyInputTest()
        {
            var response = _manager.UpdateEvent(new EventUpdateModel[0]);

            Assert.Empty(response);
        }

        [Fact]
        public void UpdateNullInInputTest()
        {
            var entities = SeedEvents(DbContext);

            var updateModels = entities.Select(x => new EventUpdateModel
            {
                Id = x.Id,
                Date = DateTimeOffset.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            }).ToList();

            updateModels.Add((EventUpdateModel)null);

            var response = _manager.UpdateEvent(updateModels.ToArray()).ToList();

            var updatedEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(updateModels.Count() - 1, response.Count());
            AssertUpdated(updateModels.Where(x => x != null).ToList(), updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        [Fact]
        public void UpdateOneIdDoesntExistTest()
        {
            var entities = SeedEvents(DbContext);

            var updateModels = entities.Select(x => new EventUpdateModel
            {
                Id = x.Id,
                Date = DateTimeOffset.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            }).ToList();

            updateModels.Add(new EventUpdateModel
            {
                Id = DbContext.Set<Event>().AsNoTracking().Max(x => x.Id) + 1,
                Date = DateTimeOffset.Now,
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            });

            var response = _manager.UpdateEvent(updateModels.ToArray()).ToList();

            var updatedEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(updateModels.Count() - 1, response.Count());
            AssertUpdated(updateModels.Where(x => response.Contains(x.Id)).ToList(), updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        private static IEnumerable<EventCreateModel> GetValidModels()
        {
            for(var i = 0; i < 5; i ++)
            {
                yield return new EventCreateModel
                {
                    Date = DateTimeOffset.Now.AddDays(i * -1),
                    Title = $"title{i}",
                    Description = $"description{i}"
                };
            }
        }

        private static IEnumerable<Event> SeedEvents(ApiDbContext dbContext)
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
            dbContext.AddRange(eventList);
            dbContext.SaveChanges();

            return eventList;
        }

        private static void AssertCreated(IList<EventCreateModel> models, IList<Event> entities)
        {
            Assert.Equal(models.Count, entities.Count);

            var editableEntityList = entities.ToList();
            // TODO : ForEach on IList, IEnumerable in tools
            foreach(var model in models)
            {
                var entity = editableEntityList.FirstOrDefault(x => x.Date.Equals(model.Date.Date) && 
                    model.Title.EqualOrdinal(x.Title) && 
                    model.Description.EqualOrdinal(x.Description));

                Assert.NotNull(entity);

                editableEntityList.Remove(entity);
            }
        }

        private static void AssertUpdated(IList<EventUpdateModel> models, IList<Event> entities)
        {
            Assert.Equal(models.Count, entities.Count);

            // TODO : ForEach on IList, IEnumerable in tools
            foreach(var model in models)
            {
                var entity = entities.FirstOrDefault(x => x.Id.Equals(model.Id));

                Assert.NotNull(entity);

                Assert.Equal(model.Date.Date, entity.Date);
                Assert.Equal(model.Description, entity.Description);
                Assert.Equal(model.Title, entity.Title);
            }
        }

        private static void AssertResponseCorrect(IList<int> response, IList<Event> entities)
        {
            Assert.Equal(response.Count, entities.Count);

            Assert.Equal(response.OrderBy(x => x), entities.Select(x => x.Id).OrderBy(x => x));
        }
    }
}
