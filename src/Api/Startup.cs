namespace DetroitHarps.Api
{
    using DetroitHarps.Business;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
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
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("doc", new Info { Title = "DetroitHarps API", Version = "v1" });
            });

            var connectionString = _config.GetConnectionString(ConnectionStringName);
            services.AddDbContext(connectionString);
            services.AddRepositories();
            services.AddManagers();

            services.AddAutoMapper();
        }
    }
}
