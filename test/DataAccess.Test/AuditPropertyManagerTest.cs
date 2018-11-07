namespace DetroitHarps.DataAccess.Test
{
    using System;
    using DetroitHarps.Business.Contact.Entities;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AuditPropertyManagerTest
    {
        private readonly DetroitHarpsDbContext _dbContext;

        public AuditPropertyManagerTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public void SetsTimestampsOnInsertTest()
        {
            var auditPropertyManager = new AuditPropertyManager();

            // can use any EntityWithId
            var message = new Message();
            _dbContext.Add(message);

            var before = DateTimeOffset.Now;

            // TODO: see todo on interface IAuditPropertManager
            auditPropertyManager.SetTimestamps(_dbContext);

            var after = DateTimeOffset.Now;

            Assert.InRange(_dbContext.Entry(message).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue, before, after);
            Assert.InRange(_dbContext.Entry(message).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue, before, after);
            Assert.Equal(
                _dbContext.Entry(message).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue,
                _dbContext.Entry(message).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue);
        }

        [Fact]
        public void SetsTimestampsOnUpdateTest()
        {
            var auditPropertyManager = new AuditPropertyManager();

            // can use any EntityWithId
            var message = new Message();
            _dbContext.Add(message);
            _dbContext.SaveChanges();

            _dbContext.Entry(message).State = EntityState.Modified;

            var before = DateTimeOffset.Now;

            // TODO: see todo on interface IAuditPropertManager
            auditPropertyManager.SetTimestamps(_dbContext);

            var after = DateTimeOffset.Now;

            Assert.NotInRange(_dbContext.Entry(message).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue, before, after);
            Assert.InRange(_dbContext.Entry(message).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue, before, after);
            Assert.NotEqual(
                _dbContext.Entry(message).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue,
                _dbContext.Entry(message).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue);
        }
    }
}