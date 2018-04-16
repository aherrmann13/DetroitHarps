
namespace Repository.Migrator
{
    using System;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using Repository;

    public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
    {  
        private readonly MigratorOptions _options;

        public ApiDbContextFactory()
        {
            var config = CreateConfiguration();

            _options = config.GetSection(nameof(MigratorOptions)).Get<MigratorOptions>();
        }

        public ApiDbContext CreateDbContext() => CreateDbContext(null);

        public ApiDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApiDbContext>()
                .UseNpgsql(_options.ConnectionString, b => 
                    b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
            return new ApiDbContext(builder.Options);
        }

        private static IConfiguration CreateConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if DEBUG
                .AddJsonFile("appsettings.debug.json", optional: true, reloadOnChange: true)
#endif
                .Build();
    }
}