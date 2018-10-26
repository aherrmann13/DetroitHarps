namespace DetroitHarps.DataAccess.Test.EntityBuilders
{
    using System;
    using System.Linq;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class RegistrationEntityBuilderTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public RegistrationEntityBuilderTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void ThrowsOnNullModelBuilderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new RegistrationEntityBuilder(null));
        }

        [Fact]
        public void RegistrationTableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(Registration))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Registration), tableName);
        }

        [Fact]
        public void RegistrationParentTableNotCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(RegistrationParent))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Registration), tableName);
        }

        [Fact]
        public void RegistrationContactInformationTableNotCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(RegistrationContactInformation))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Registration), tableName);
        }

        [Fact]
        public void RegistrationPaymentInformationTableNotCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(RegistrationPaymentInformation))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Registration), tableName);
        }
    }
}