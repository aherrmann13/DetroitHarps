namespace DetroitHarps.DataAccess.Test.EntityBuilders
{
    using System;
    using System.Linq;
    using DetroitHarps.Business.Schedule.Entities;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class EventEntityBuilderTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public EventEntityBuilderTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void ThrowsOnNullModelBuilderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EventEntityBuilder(null));
        }

        [Fact]
        public void TableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(Event))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Event), tableName);
        }
    }
}