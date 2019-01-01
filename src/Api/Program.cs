namespace DetroitHarps.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        private const string Appsettings = "appsettings.yaml";

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
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
                .Build();
    }
}