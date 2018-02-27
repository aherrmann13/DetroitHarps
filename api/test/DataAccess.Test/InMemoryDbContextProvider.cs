namespace DataAccess.Test
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    using Xunit;

    public class InMemoryDbContextProvider
    {
        public static ApiDbContext GetContext() => new ApiDbContext(CreateNewContextOptions());

        private static DbContextOptions<ApiDbContext> CreateNewContextOptions(string name = null)
        {
            name = name ?? Guid.NewGuid().ToString();

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(name)
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
    }
}
