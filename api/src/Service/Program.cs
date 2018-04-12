namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public class Program
    {
        public static void Main(string[] args)
        {
            var config = CreateConfiguration();
            var serviceOptions = config.GetSection(ServiceOptions.SectionName).Get<ServiceOptions>();

            new WebHostBuilder()
                .UseKestrel()
                .UseCustomLogging(config, serviceOptions, enableConsole: true)
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
        
        private static IConfiguration CreateConfiguration() =>
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.debug.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        
    }
}
