namespace Repository.Test
{
    using System;
    using DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Repository;
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
