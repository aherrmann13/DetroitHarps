namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Interfaces;
    using Business.Managers;
    using Business.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Repository;
    using Swashbuckle.AspNetCore.Swagger;
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

            services.AddSwaggerGen(options => 
            {
                // TODO documentation files
                options.SwaggerDoc("doc", new Info() { Version = "v1", Title = $"{_serviceOptions.ServiceName}"});
            });

            //TODO:add sql query logging
            services.AddDbContext<ApiDbContext>(ServiceLifetime.Scoped);

            var repositoryOptions = _configuration.GetSection(nameof(RepositoryOptions)).Get<RepositoryOptions>();
            services.AddSingleton(x => 
                new DbContextOptionsBuilder<ApiDbContext>()
                    .UseNpgsql(repositoryOptions.ConnectionString)
                    .Options);

            var contactManagerOptions = _configuration.GetSection(nameof(ContactManagerOptions)).Get<ContactManagerOptions>();
            services.AddSingleton(contactManagerOptions);

            // TODO only in dev
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.AddTransient<IPhotoGroupManager, PhotoGroupManager>();
            services.AddTransient<IPhotoManager, PhotoManager>();
            services.AddTransient<IRegistrationManager, RegistrationManager>();
            services.AddTransient<IScheduleManager, ScheduleManager>();
            services.AddTransient<IContactManager, ContactManager>();

            var stripeManagerMock = new Mock<IStripeManager>();
            stripeManagerMock.Setup(x => x.Charge(It.IsAny<StripeChargeModel>()))
                .Returns("paypal");
            
            services.AddSingleton(stripeManagerMock.Object);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseCustomLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseSwagger();
            app.UseStaticFiles();
            
            app.UseMvc();
        }
    }
}
