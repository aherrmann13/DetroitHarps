namespace DetroitHarps.DataAccess.Test.EntityBuilders
{
    using System;
    using System.Linq;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class MessageEntityBuilderTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public MessageEntityBuilderTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void ThrowsOnNullModelBuilderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new MessageEntityBuilder(null));
        }

        [Fact]
        public void TableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(Message))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Message), tableName);
        }

         [Fact]
        public void AuditPropertiesAddedTest()
        {
            var insertTimestampColumnName = _dbContext.Model.FindEntityType(typeof(Message))?.FindProperty(Constants.InsertTimestampPropertyName);
            var updateTimestampColumnName = _dbContext.Model.FindEntityType(typeof(Message))?.FindProperty(Constants.UpdateTimestampPropertyName);

            Assert.NotNull(insertTimestampColumnName);
            Assert.NotNull(updateTimestampColumnName);
        }
    }
}