namespace DetroitHarps.Api.Authentication
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    using Tools;

    public class Auth0Configurator
    {
        private readonly Auth0Settings _settings;

        public Auth0Configurator(Auth0Settings settings)
        {
            Guard.NotNull(settings, nameof(settings));

            _settings = settings;
        }

        public void Apply(IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = _settings.Authority;
                options.Audience = _settings.ApiIdentifier;
            });
        }
    }
}