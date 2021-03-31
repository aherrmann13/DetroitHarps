namespace DetroitHarps.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        private const string Appsettings = "appsettings.yaml";
        private const string LocalAppsettings = "appsettings.local.yaml";

        public static void Main(string[] args)
        {
            var config = CreateConfiguration();
            var serviceOptions =
                config.GetSection(ServiceOptions.SectionName).Get<ServiceOptions>();

            new WebHostBuilder()
                .UseKestrel()
                .UseCustomLogging(config, serviceOptions)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddSingleton(config);
                    serviceCollection.AddSingleton(serviceOptions);
                })
                .UseUrls(serviceOptions.BindUrl)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(CreateConfiguration());
                });

        private static IConfiguration CreateConfiguration() =>
            new ConfigurationBuilder()
                .AddYamlFile(Appsettings, optional: false, reloadOnChange: true)
#if DEBUG
                .AddYamlFile(LocalAppsettings, optional: true, reloadOnChange: false)
#endif
                .AddEnvironmentVariables()
                .Build();
    }
}