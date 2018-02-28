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

            var response = _manager.CreateEvent(createModels);

            var entities = DbContext.Set<Event>()
                .AsNoTracking()
                .ToList();

            Assert.Equal(createModels.Count(), response.Count());
            Assert.Equal(createModels.Count(), entities.Count());
            Assert.Equal(response, entities.Select(x => x.Id));
        }

        private IEnumerable<EventCreateModel> GetValidModels()
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
    }
}
