namespace Microsoft.AspNetCore.Hosting
{
    using DetroitHarps.Api;
    using DetroitHarps.Api.Services.ClientLogging;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Filters;
    using Serilog.Formatting.Compact;

    // TODO: this whole class needs to be cleaned up
    public static class WebHostBuilderExtensions
    {
        private const string DefaultConsoleTemplate = "{Timestamp:o} [{Level:u3}] {SourceContext:l} {Scope} {Application} {Message}{NewLine}{Exception}";
        private const string DefaultFileTemplate = "{Timestamp:o} [{Level:u3}] {SourceContext:l} {Scope} ({Application}/{MachineName}/{ThreadId}) {Message}{NewLine}{Exception}";

        public static IWebHostBuilder UseCustomLogging(
            this IWebHostBuilder webHostBuilder,
            IConfiguration configuration,
            ServiceOptions serviceOptions)
        {
            return webHostBuilder.UseSerilog(
                (hostingContext, loggerConfig) =>
                {
                    loggerConfig.ReadFrom.Configuration(configuration);
                    var apiRollingLogFile = $"{serviceOptions.ServiceName}-{{Date}}.json";
                    var apiRollingLogErrorFile = $"{serviceOptions.ServiceName}-{{Date}}-Errors.log";
                    var clientRollingLogFile = $"{serviceOptions.ServiceName}-{{Date}}.json";
                    var clientRollingLogErrorFile = $"{serviceOptions.ServiceName}-{{Date}}-Errors.log";

                    if (!string.IsNullOrWhiteSpace(serviceOptions.LogFolder))
                    {
                        // TODO Ends with ordinal in Tools?
                        // TODO this slash is different in windows and linux
                        var apiFolder = serviceOptions.LogFolder.EndsWith('/')
                            ? serviceOptions.LogFolder
                            : serviceOptions.LogFolder + "/";

                        var clientFolder = serviceOptions.ClientLogFolder.EndsWith('/')
                            ? serviceOptions.ClientLogFolder
                            : serviceOptions.ClientLogFolder + "/";

                        apiRollingLogFile = apiFolder + apiRollingLogFile;
                        apiRollingLogErrorFile = apiFolder + apiRollingLogErrorFile;
                        clientRollingLogFile = clientFolder + clientRollingLogFile;
                        clientRollingLogErrorFile = clientFolder + clientRollingLogErrorFile;
                    }

                    if (serviceOptions.EnableConsole)
                    {
                        loggerConfig.WriteTo.LiterateConsole(outputTemplate: DefaultConsoleTemplate);
                    }

                    loggerConfig
                        .WriteTo.Logger(logger => logger
                            .Filter.ByExcluding(Matching.FromSource(typeof(ClientLoggerFacade).FullName))
                            .WriteTo.RollingFile(
                                formatter: new CompactJsonFormatter(),
                                pathFormat: apiRollingLogFile,
                                retainedFileCountLimit: 5))
                        .WriteTo.Logger(logger => logger
                            .Filter.ByExcluding(Matching.FromSource(typeof(ClientLoggerFacade).FullName))
                            .WriteTo.RollingFile(
                                pathFormat: apiRollingLogErrorFile,
                                outputTemplate: DefaultFileTemplate,
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                                retainedFileCountLimit: 31,
                                buffered: false,
                                shared: false))
                        .WriteTo.Logger(logger => logger
                            .Filter.ByIncludingOnly(Matching.FromSource(typeof(ClientLoggerFacade).FullName))
                            .WriteTo.RollingFile(
                                formatter: new CompactJsonFormatter(),
                                pathFormat: clientRollingLogFile,
                                retainedFileCountLimit: 5))
                        .WriteTo.Logger(logger => logger
                            .Filter.ByIncludingOnly(Matching.FromSource(typeof(ClientLoggerFacade).FullName))
                            .WriteTo.RollingFile(
                                pathFormat: clientRollingLogErrorFile,
                                outputTemplate: DefaultFileTemplate,
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                                retainedFileCountLimit: 31,
                                buffered: false,
                                shared: false));
                });
        }
    }
}