namespace DetroitHarps.DataAccess.Test
{
    using System;
    using DetroitHarps.Business.Photo.Entities;
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
            var photoGroup = new PhotoGroup();
            _dbContext.Add(photoGroup);

            var before = DateTimeOffset.Now;

            // TODO: see todo on interface IAuditPropertManager
            auditPropertyManager.SetTimestamps(_dbContext);

            var after = DateTimeOffset.Now;

            Assert.InRange(_dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue, before, after);
            Assert.InRange(_dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue, before, after);
            Assert.Equal(
                _dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue,
                _dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue);
        }

        [Fact]
        public void SetsTimestampsOnUpdateTest()
        {
            var auditPropertyManager = new AuditPropertyManager();

            // can use any EntityWithId
            var photoGroup = new PhotoGroup();
            _dbContext.Add(photoGroup);
            _dbContext.SaveChanges();

            _dbContext.Entry(photoGroup).State = EntityState.Modified;

            var before = DateTimeOffset.Now;

            // TODO: see todo on interface IAuditPropertManager
            auditPropertyManager.SetTimestamps(_dbContext);

            var after = DateTimeOffset.Now;

            Assert.NotInRange(_dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue, before, after);
            Assert.InRange(_dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue, before, after);
            Assert.NotEqual(
                _dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue,
                _dbContext.Entry(photoGroup).Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue);
        }
    }
}