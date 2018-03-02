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

            var response = _manager.Create(createModels).ToList();

            var entities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            AssertCreated(createModels, entities);
            AssertResponseCorrect(response, entities);
        }

        [Fact]
        public void CreateNullInputTest()
        {
            var response = _manager.Create((EventCreateModel[])null);

            Assert.Empty(response);
        }

        [Fact]
        public void CreateEmptyInputTest()
        {
            var response = _manager.Create(new EventCreateModel[0]);

            Assert.Empty(response);
        }

        [Fact]
        public void CreateNullInInputTest()
        {
            var createModels = GetValidModels().ToList();
            createModels.Add((EventCreateModel)null);

            var response = _manager.Create(createModels.ToArray()).ToList();

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

            var response = _manager.Update(updateModels).ToList();

            var updatedEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            AssertUpdated(updateModels, updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }

        [Fact]
        public void UpdateNullInputTest()
        {
            var response = _manager.Update((EventUpdateModel[])null);

            Assert.Empty(response);
        }

        [Fact]
        public void UpdateEmptyInputTest()
        {
            var response = _manager.Update(new EventUpdateModel[0]);

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

            var response = _manager.Update(updateModels.ToArray()).ToList();

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

            var response = _manager.Update(updateModels.ToArray()).ToList();

            var updatedEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(updateModels.Count() - 1, response.Count());
            AssertUpdated(updateModels.Where(x => response.Contains(x.Id)).ToList(), updatedEntities);
            AssertResponseCorrect(response, updatedEntities);
        }
        
        [Fact]
        public void DeleteSuccess()
        {
            var entities = SeedEvents(DbContext);

            var deleteIds = entities.Select(x => x.Id).Take(3).ToArray();

            var response = _manager.Delete(deleteIds);

            var remainingEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(response, deleteIds);
            Assert.Equal(entities.Count() - response.Count(), remainingEntities.Count);

            Assert.Empty(remainingEntities.Where(x => deleteIds.Contains(x.Id)));
        }

        [Fact]
        public void DeleteNullInputTest()
        {
            var response = _manager.Delete((int[])null);

            Assert.Empty(response);
        }

        [Fact]
        public void DeleteEmptyInputTest()
        {
            var response = _manager.Delete(new int[0]);

            Assert.Empty(response);
        }

        [Fact]
        public void DeleteOneIdDoesntExistTest()
        {
            var entities = SeedEvents(DbContext);

            var deleteIds = entities.Select(x => x.Id).Take(3).ToList();

            deleteIds.Add(DbContext.Set<Event>().AsNoTracking().Max(x => x.Id) + 1);

            var response = _manager.Delete(deleteIds.ToArray()).ToList();

            var remainingEntities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(response.Count, deleteIds.Count - 1);
            Assert.Equal(entities.Count() - response.Count(), remainingEntities.Count);

            Assert.Empty(remainingEntities.Where(x => deleteIds.Contains(x.Id)));
        }

        [Fact]
        public void GetAllSuccessTest()
        {
            var entities = SeedEvents(DbContext);

            var response = _manager.GetAll();

            AssertEqual(response, entities);
        }

        [Fact]
        public void GetSuccessTest()
        {
            var entities = SeedEvents(DbContext);
            var ids = entities.Select(x => x.Id).Take(3).ToList();

            var response = _manager.Get(ids.ToArray());

            AssertEqual(response, entities.Where(x => ids.Contains(x.Id)));
        }

        [Fact]
        public void GetSomeIdsExistTest()
        {
            var entities = SeedEvents(DbContext);
            var ids = entities.Select(x => x.Id).Take(3).ToList();

            ids.Add(DbContext.Set<Event>().AsNoTracking().Max(x => x.Id) + 1);

            var response = _manager.Get(ids.ToArray());

            Assert.Equal(response.Count(), ids.Count - 1);
            AssertEqual(response, entities.Where(x => ids.Contains(x.Id)));
        }

        [Fact]
        public void GetNoIdsExistTest()
        {
            var entities = SeedEvents(DbContext);

            var ids = new List<int>
            {
                DbContext.Set<Event>().AsNoTracking().Max(x => x.Id) + 1,
                DbContext.Set<Event>().AsNoTracking().Max(x => x.Id) + 2
            };

            var response = _manager.Get(ids.ToArray());

            Assert.Empty(response);
        }

        [Fact]
        public void GetNullInputTest()
        {
            var entities = SeedEvents(DbContext);

            var response = _manager.Get((int[])null);

            Assert.Empty(response);
        }

        [Fact]
        public void GetEmptyInputTest()
        {
            var entities = SeedEvents(DbContext);

            var response = _manager.Get(new int[0]);

            Assert.Empty(response);
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

        private static void AssertCreated(IEnumerable<EventCreateModel> models, IEnumerable<Event> entities)
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

        private static void AssertUpdated(IEnumerable<EventUpdateModel> models, IEnumerable<Event> entities)
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

        private static void AssertResponseCorrect(IList<int> response, IList<Event> entities)
        {
            Assert.Equal(response.Count, entities.Count);

            Assert.Equal(response.OrderBy(x => x), entities.Select(x => x.Id).OrderBy(x => x));
        }
    }
}
