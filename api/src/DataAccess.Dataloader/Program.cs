namespace DataAccess.Dataloader
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using DataAccess.Dataloader.DataUnit;
    using DataAccess.Migrator;
    using Serilog;

    public class Program
    {
        static void Main(string[] args)
        {
            var config = CreateConfiguration();
            var options = config.GetSection(nameof(DataloaderOptions)).Get<DataloaderOptions>();
            options.LogFolder = options.LogFolder.EndsWith("/") ? options.LogFolder : options.LogFolder + "/";
            var logFile = options.LogFolder + "/" + $"Dataloader-{DateTime.Now:yyyyMMdd}.txt";

            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFile)
#if DEBUG
                .WriteTo.Console()
#endif
                .CreateLogger();

            var provider = BuildServiceProvider(options);

            try
            {
                // TODO add logging to the actual data units
                logger.Information("starting");
                var dataUnits = provider.GetServices<IDataUnit>();
                foreach(var unit in dataUnits)
                {
                    unit.Run(clearExisting: true);
                }
                logger.Information("done");
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        private static IServiceProvider BuildServiceProvider(DataloaderOptions options) =>
            new ServiceCollection()
                .AddDbContext<ApiDbContext>(ServiceLifetime.Transient)
                .AddSingleton(x => 
                    new DbContextOptionsBuilder<ApiDbContext>()
                        .UseNpgsql(options.ConnectionString)
                        .Options)
                .AddSingleton(options)
                .AddTransient<IDataUnit, EventDataUnit>()
                .AddTransient<IDataUnit, UserDataUnit>()
                .AddTransient<IDataUnit, PhotoDataUnit>()
                .AddTransient<IDataUnit, SeasonDataUnit>()
                .BuildServiceProvider();

        private static IConfiguration CreateConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if DEBUG
                .AddJsonFile("appsettings.debug.json", optional: true, reloadOnChange: true)
#endif
                .Build();
    }
}
