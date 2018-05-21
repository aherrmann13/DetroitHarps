namespace Service
{
    using System.Linq;
    using Business.Interfaces;
    using Business.Managers;
    using Business.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Repository;
    using Service.Models;
    using Swashbuckle.AspNetCore.Swagger;
    using Tools;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ServiceOptions _serviceOptions;


        public Startup(IConfiguration configuration, ServiceOptions serviceOptions)
        {
            Guard.NotNull(configuration, nameof(configuration));
            Guard.NotNull(serviceOptions, nameof(serviceOptions));
            
            _configuration = configuration;
            _serviceOptions = serviceOptions;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(options => 
            {
                // TODO: documentation files
                options.SwaggerDoc("doc", new Info() { Version = "v1", Title = $"{_serviceOptions.ServiceName}"});
                options.DescribeAllEnumsAsStrings();
            });

            // TODO: add sql query logging
            services.AddDbContext<ApiDbContext>(ServiceLifetime.Scoped);

            var repositoryOptions = _configuration.GetSection(nameof(RepositoryOptions)).Get<RepositoryOptions>();
            services.AddSingleton(x => 
                new DbContextOptionsBuilder<ApiDbContext>()
                    .UseNpgsql(repositoryOptions.ConnectionString)
                    .Options);

            var contactManagerOptions = _configuration.GetSection(nameof(ContactManagerOptions)).Get<ContactManagerOptions>();
            services.AddSingleton(contactManagerOptions);

            services.AddCors(o =>
                o.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(_serviceOptions.CorsAllowUrls.ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));

            services.AddTransient<IPhotoGroupManager, PhotoGroupManager>();
            services.AddTransient<IPhotoManager, PhotoManager>();
            services.AddTransient<IRegistrationManager, RegistrationManager>();
            services.AddTransient<IScheduleManager, ScheduleManager>();
            services.AddTransient<IContactManager, ContactManager>();
            services.AddTransient<IUserManager, UserManager>();

            var stripeManagerMock = new Mock<IStripeManager>();
            stripeManagerMock.Setup(x => x.Charge(It.IsAny<StripeChargeModel>()))
                .Returns("paypal");

            services.AddSingleton(stripeManagerMock.Object);

            services.AddCustomJwtAuthentication(_configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseCustomLogging();

            app.UseAuthentication();
            
            app.UseSwagger();
            app.UseStaticFiles();
            
            app.UseMvc();
        }
    }
}
