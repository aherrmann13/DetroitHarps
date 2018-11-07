namespace DetroitHarps.DataAccess.Test
{
    using System.Linq;
    using DetroitHarps.Business.Contact.Entities;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class DetroitHarpsDbContextTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public DetroitHarpsDbContextTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
        }

        [Fact]
        public void CreateDbTest()
        {
            Assert.True(_dbContext.Database.EnsureCreated());
        }

        [Fact]
        public void EnsureAllEntitiesCreatedTest()
        {
            _dbContext.Database.EnsureCreated();

            var types = _dbContext.Model.GetEntityTypes();

            Assert.Equal(11, types.Count());
        }

        [Fact]
        public void EnsureAuditPropertyCalled()
        {
            _dbContext.Database.EnsureCreated();

            var mock = new Mock<IAuditPropertyManager>();

            _dbContext.AuditPropertyManager = mock.Object;

            _dbContext.SaveChanges();

           mock.Verify(x => x.SetTimestamps(It.Is<DbContext>(y => y.Equals(_dbContext))));
        }
    }
}