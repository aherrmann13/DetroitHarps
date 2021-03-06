﻿namespace DetroitHarps.Api
{
    using System.Linq;
    using DetroitHarps.Api.Authentication;
    using DetroitHarps.Api.Services.Email;
    using DetroitHarps.Api.Settings;
    using DetroitHarps.DataAccess;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json.Converters;
    using Tools;

    public class Startup
    {
        // TODO: better way of managing this
        private const string ConnectionStringName = "Default";
        private const string CorsPolicyName = "CorsPolicy";

        private readonly IConfiguration _config;
        private readonly ServiceOptions _options;

        public Startup(IConfiguration configuration, ServiceOptions options)
        {
            Guard.NotNull(configuration, nameof(configuration));
            Guard.NotNull(options, nameof(options));

            _config = configuration;
            _options = options;
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

            app.UseHealthChecks("/health");

            app.UseCors(CorsPolicyName);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/doc/swagger.json", "Detroit Harps API");
                c.DisplayOperationId();
            });

            app.UseStaticFiles();

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
            .AddMvc(config =>
            {
                // protect by default, AllowAnonymous where necessary
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddJsonOptions(options =>
            {
                options
                    .SerializerSettings
                    .Converters
                    .Add(new StringEnumConverter());
            });

            services.AddSwagger();

            var connectionString = _config.GetConnectionString(ConnectionStringName);
            var s3Settings = _config
                .GetSection(S3Settings.SectionName)
                .Get<S3Settings>();

            services.AddDbContext(connectionString);
            services.AddS3ObjectStores(s3Settings);
            services.AddRepositories();
            services.AddManagers();
            services.AddCsvWriter();

            var auth0Settings = _config
                .GetSection(Auth0Settings.SectionName)
                .Get<Auth0Settings>();

            services.AddAuth0(auth0Settings);

            services.AddAutoMapper();

            services.AddClientLogger();

            services.AddHealthChecks()
                .AddDbContextCheck<DetroitHarpsDbContext>();

            var emailSettings = _config
                .GetSection(EmailSettings.SectionName)
                .Get<EmailSettings>();
            services.AddEmailSender(emailSettings);

            services.AddCors(o =>
                o.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.WithOrigins(_options.CorsAllowUrls.ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));
        }
    }
}