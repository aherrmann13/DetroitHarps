namespace DetroitHarps.DataAccess
{
    using System;
    using System.Linq;
    using DetroitHarps.Business;
    using Microsoft.EntityFrameworkCore;

    public class AuditPropertyManager : IAuditPropertyManager
    {
        public void SetTimestamps(DbContext dbContext)
        {
            var now = DateTimeOffset.Now;
            dbContext.ChangeTracker.Entries<IHasId<int>>()
                .Where(x => x.State == EntityState.Added)
                .ToList()
                .ForEach(x =>
                {
                    x.Property<DateTimeOffset>(Constants.InsertTimestampPropertyName).CurrentValue = now;
                    x.Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue = now;
                });

            dbContext.ChangeTracker.Entries<IHasId<int>>()
                .Where(x => x.State == EntityState.Modified)
                .ToList()
                .ForEach(x =>
                {
                    x.Property<DateTimeOffset>(Constants.UpdateTimestampPropertyName).CurrentValue = now;
                });
        }
    }
}