namespace Microsoft.Extensions.DependencyInjection
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using DetroitHarps.Api.Authentication;
    using DetroitHarps.Api.Middleware;
    using DetroitHarps.Api.Services.ClientLogging;
    using DetroitHarps.Api.Services.Email;
    using DetroitHarps.Api.Swagger;
    using DetroitHarps.Business;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.DataAccess;
    using DetroitHarps.Repository;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Swashbuckle.AspNetCore.Swagger;
    using Tools;
    using Tools.Csv;

    // purpose of this class is to make Startup.cs
    // explicit in what is being set up
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(
            this IServiceCollection services,
            string connectionString)
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNullOrWhiteSpace(connectionString, nameof(connectionString));

            services.AddDbContext<DetroitHarpsDbContext>(
                options =>
                {
                    options.UseNpgsql(connectionString);
                },
                ServiceLifetime.Scoped,
                ServiceLifetime.Singleton);

            return services;
        }

        public static IServiceCollection AddManagers(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddScoped<IContactManager, ContactManager>();
            services.AddScoped<IPhotoManager, PhotoManager>();
            services.AddScoped<IPhotoGroupManager, PhotoGroupManager>();
            services.AddScoped<IRegistrationManager, RegistrationManager>();
            services.AddScoped<IScheduleManager, ScheduleManager>();

            return services;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IPhotoGroupRepository, PhotoGroupRepository>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            // TODO: correct place for this?
            Mapper.Initialize(BusinessMapperConfiguration.Configure);
            Mapper.AssertConfigurationIsValid();

            return services;
        }

        public static IServiceCollection AddSwagger(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("doc", new Info { Title = "DetroitHarps API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                var apiScheme = new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                };

                c.AddSecurityDefinition(
                    "Bearer",
                    apiScheme);
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", Enumerable.Empty<string>() },
                });
                c.EnableAnnotations();
                c.SchemaFilter<AssignPropertyRequiredFilter>();
                c.OperationFilter<FormFileSwaggerFilter>();
                c.MapType<FileResult>(() => new Schema
                    {
                        Type = "file"
                    });
            });

            return services;
        }

        public static IServiceCollection AddAuth0(
            this IServiceCollection services,
            Auth0Settings settings)
        {
            Guard.NotNull(services, nameof(services));

            var auth0 = new Auth0Configurator(settings);
            auth0.Apply(services);

            return services;
        }

        public static IServiceCollection AddCsvWriter(this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddSingleton<ICsvFormatter, CsvFormatter>();
            services.AddSingleton<ICsvWriter, CsvWriter>();

            return services;
        }

        public static IServiceCollection AddEmailSender(
            this IServiceCollection services,
            EmailSettings settings)
        {
            Guard.NotNull(services, nameof(services));

            services.AddSingleton<EmailSettings>(settings);
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }

        public static IServiceCollection AddClientLogger(this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddTransient<ClientLoggerFacade>();

            return services;
        }
    }
}