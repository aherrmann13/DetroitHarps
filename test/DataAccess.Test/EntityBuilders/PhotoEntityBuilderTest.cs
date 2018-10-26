namespace DetroitHarps.DataAccess.Test.EntityBuilders
{
    using System;
    using System.Linq;
    using DetroitHarps.Business.Photo.Entities;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class PhotoEntityBuilderTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public PhotoEntityBuilderTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void ThrowsOnNullModelBuilderTest()
        {
            Assert.Throws<ArgumentNullException>(() => new PhotoEntityBuilder(null));
        }

        [Fact]
        public void PhotoTableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(Photo))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Photo), tableName);
        }

        [Fact]
        public void PhotoDisplayPropertiesTableNotCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(PhotoDisplayProperties))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(Photo), tableName);
        }

        [Fact]
        public void PhotoDataTableCreatedTest()
        {
            var tableName = _dbContext.Model.FindEntityType(typeof(PhotoData))?.Relational()?.TableName;

            Assert.NotNull(tableName);
            Assert.Equal(nameof(PhotoData), tableName);
        }
    }
}