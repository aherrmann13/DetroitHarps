namespace Microsoft.Extensions.DependencyInjection
{
    using DetroitHarps.Api.Middleware;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Repository;
    using Microsoft.AspNetCore.Builder;
    using Tools;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContext(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            // TODO: dbcontext config here
            return services;
        }

        public static IServiceCollection AddManagers(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddScoped<IScheduleManager, ScheduleManager>();

            return services;
        }

        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddScoped<IEventRepository, EventRepository>();

            return services;
        }
    }
}