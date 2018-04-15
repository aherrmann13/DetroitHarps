namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Managers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Repository;
    using Tools;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceOptions _serviceOptions;
        private readonly ILoggerFactory _loggerFactory;


        public Startup(IConfiguration configuration, ServiceOptions serviceOptions, ILoggerFactory loggerFactory)
        {
            Guard.NotNull(configuration, nameof(configuration));
            Guard.NotNull(serviceOptions, nameof(serviceOptions));
            Guard.NotNull(loggerFactory, nameof(loggerFactory));
            
            _configuration = configuration;
            _serviceOptions = serviceOptions;
            _loggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //TODO:add sql query logging
            services.AddDbContext<ApiDbContext>(ServiceLifetime.Scoped);

            var repositoryOptions = _configuration.GetSection(RepositoryOptions.SectionName).Get<RepositoryOptions>();
            services.AddSingleton(x => 
                new DbContextOptionsBuilder<ApiDbContext>()
                    .UseNpgsql(repositoryOptions.ConnectionString)
                    .Options);

            services.AddTransient<IPhotoGroupManager, PhotoGroupManager>();
            services.AddTransient<IPhotoManager, PhotoManager>();
            services.AddTransient<IRegistrationManager, RegistrationManager>();
            services.AddTransient<IScheduleManager, ScheduleManager>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
