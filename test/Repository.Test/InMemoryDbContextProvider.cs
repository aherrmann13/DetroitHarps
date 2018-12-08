namespace DetroitHarps.Repository.Test
{
    using System;
    using DetroitHarps.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class InMemoryDbContextProvider
    {
        public static DetroitHarpsDbContext GetContext() => new DetroitHarpsDbContext(CreateNewContextOptions());

        private static DbContextOptions<DetroitHarpsDbContext> CreateNewContextOptions(string name = null)
        {
            name = name ?? Guid.NewGuid().ToString();

            var builder = new DbContextOptionsBuilder<DetroitHarpsDbContext>()
                .UseInMemoryDatabase(name);

            return builder.Options;
        }
    }
}