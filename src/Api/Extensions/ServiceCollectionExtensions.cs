namespace Microsoft.Extensions.DependencyInjection
{
    using AutoMapper;
    using DetroitHarps.Api.Middleware;
    using DetroitHarps.Business;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.DataAccess;
    using DetroitHarps.Repository;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Tools;

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
    }
}