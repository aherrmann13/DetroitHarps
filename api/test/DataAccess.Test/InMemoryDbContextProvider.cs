namespace DataAccess.Test
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using DataAccess;
    using Xunit;

    public class InMemoryDbContextProvider
    {
        public static ApiDbContext GetContext() => new ApiDbContext(CreateNewContextOptions());

        private static DbContextOptions<ApiDbContext> CreateNewContextOptions(string name = null)
        {
            name = name ?? Guid.NewGuid().ToString();

            var builder = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(name);

            return builder.Options;
        }
    }
}
