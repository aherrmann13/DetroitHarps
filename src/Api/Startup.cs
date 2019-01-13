﻿namespace DetroitHarps.Api
{
    using DetroitHarps.Api.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Tools;

    public class Startup
    {
        // TODO: better way of managing this
        private const string ConnectionStringName = "Default";
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            Guard.NotNull(configuration, nameof(configuration));

            _config = configuration;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddRequestLogging();
            app.AddExceptionHandler();

            app.AddAuth0();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/doc/swagger.json", "Detroit Harps API");
            });

            app.UseStaticFiles();

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
                // protect by default, AllowAnonymous where necessary
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddSwagger();

            var connectionString = _config.GetConnectionString(ConnectionStringName);
            services.AddDbContext(connectionString);
            services.AddRepositories();
            services.AddManagers();

            var auth0Settings = _config
                .GetSection(Auth0Settings.SectionName)
                .Get<Auth0Settings>();
            
            services.AddAuth0(auth0Settings);

            services.AddAutoMapper();
        }
    }
}
