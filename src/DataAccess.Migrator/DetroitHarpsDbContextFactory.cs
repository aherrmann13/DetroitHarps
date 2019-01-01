namespace DetroitHarps.DataAccess.Migrator
{
    using System.Reflection;
    using DetroitHarps.DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    public class DetroitHarpsDbContextFactory :
        IDesignTimeDbContextFactory<DetroitHarpsDbContext>
    {
        // TODO: better way of managing this
        private const string Appsettings = "appsettings.yaml";
        private const string ConnectionStringName = "Default";
        private readonly string _connectionString;

        public DetroitHarpsDbContextFactory()
        {
            var config = CreateConfiguration();

            _connectionString = config.GetConnectionString(ConnectionStringName);
        }

        public DetroitHarpsDbContext CreateDbContext() => CreateDbContext(null);

        public DetroitHarpsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DetroitHarpsDbContext>()
                .UseNpgsql(_connectionString, b =>
                    b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
            return new DetroitHarpsDbContext(builder.Options);
        }

        private static IConfiguration CreateConfiguration() =>
            new ConfigurationBuilder()
                .AddYamlFile(Appsettings, optional: false, reloadOnChange: true)
                .Build();
    }
}