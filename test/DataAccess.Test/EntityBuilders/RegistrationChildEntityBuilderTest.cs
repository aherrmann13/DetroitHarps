namespace DetroitHarps.DataAccess.Test.EntityBuilders
{
    using System;
    using System.Linq;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RegistrationChildEntityBuilderTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public RegistrationChildEntityBuilderTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void ThrowsOnNullModelBuilderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationChildEntityBuilder(null));
        }

        [Fact]
        public void TableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(RegistrationChild))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(RegistrationChild), tableName);
        }
    }
}