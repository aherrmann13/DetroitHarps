
namespace Repository.Migrator
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Repository;

    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {
        private readonly string _connectionString = 
            $"Server=localhost;Port=5432;Database=DetroitHarps;User Id=postgres;Password=password1";
            
        public ApiDbContextFactory()
        {
        }

        public ApiDbContext CreateDbContext() => CreateDbContext(null);

        public ApiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApiDbContext>()
                .UseNpgsql(_connectionString, b => 
                    b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
            return new ApiDbContext(builder.Options);
        }
    }
}