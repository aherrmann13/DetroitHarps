namespace DetroitHarps.DataAccess.Test.EntityBuilders
{
    using System;
    using System.Linq;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PhotoGroupEntityBuilderTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public PhotoGroupEntityBuilderTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void ThrowsOnNullModelBuilderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoGroupEntityBuilder(null));
        }

        [Fact]
        public void TableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(PhotoGroup))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(PhotoGroup), tableName);
        }

         [Fact]
        public void AuditPropertiesAddedTest()
        {
            var insertTimestampColumnName = _dbContext.Model.FindEntityType(typeof(PhotoGroup))?.FindProperty(Constants.InsertTimestampPropertyName);
            var updateTimestampColumnName = _dbContext.Model.FindEntityType(typeof(PhotoGroup))?.FindProperty(Constants.UpdateTimestampPropertyName);

            Assert.NotNull(insertTimestampColumnName);
            Assert.NotNull(updateTimestampColumnName);
        }
    }
}