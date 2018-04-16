namespace Serilog
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Serilog.Formatting.Compact;
    using Service;

    public static class SerilogExtensions
    {
        private const string DefaultConsoleTemplate = "{Timestamp:o} [{Level:u3}] {SourceContext:l} {Scope} {Application} {Message}{NewLine}{Exception}";
        private const string DefaultFileTemplate = "{Timestamp:o} [{Level:u3}] {SourceContext:l} {Scope} ({Application}/{MachineName}/{ThreadId}) {Message}{NewLine}{Exception}";

        public static IWebHostBuilder UseCustomLogging(
            this IWebHostBuilder webHostBuilder,
            IConfiguration configuration,
            ServiceOptions serviceOptions,
            bool enableConsole = false)
        {
            return webHostBuilder.UseSerilog((hostingContext, loggerConfig) => 
                {
                    loggerConfig.ReadFrom.Configuration(configuration);
                    var rollingLogFile = $"{serviceOptions.ServiceName}-{{Date}}.json";
                    var rollingLogErrorFile = $"{serviceOptions.ServiceName}-{{Date}}-Errors.log";
                    
                    if(!string.IsNullOrWhiteSpace(serviceOptions.LogFolder))
                    {
                        // TODO Ends with ordinal in Tools?
                        // TODO this slash is different in windows and linux
                        var folder = serviceOptions.LogFolder.EndsWith('/')
                            ? serviceOptions.LogFolder
                            : serviceOptions.LogFolder + "/";

                        rollingLogFile = folder + rollingLogFile;
                        rollingLogErrorFile = folder + rollingLogErrorFile;
                    }
                    
                    if(enableConsole)
                    {
                        loggerConfig.WriteTo.LiterateConsole(outputTemplate: DefaultConsoleTemplate);
                    }

                    loggerConfig
                        .WriteTo.RollingFile(
                            formatter: new CompactJsonFormatter(),
                            pathFormat: rollingLogFile,
                            retainedFileCountLimit: 5)
                        .WriteTo.RollingFile(
                            pathFormat: rollingLogErrorFile,
                            outputTemplate: DefaultFileTemplate,
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                            retainedFileCountLimit: 31,
                            buffered: false,
                            shared: false
                        );
                });
        }
    }
}