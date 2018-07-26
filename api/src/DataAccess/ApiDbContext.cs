namespace DataAccess
{
    using Business.Entities;
    using DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var serviceCollection = new ServiceCollection();
            
            AddEntityBuilders(serviceCollection);
            serviceCollection.AddSingleton<ModelBuilder>(modelBuilder);

            var provider = serviceCollection.BuildServiceProvider();

            var mb = provider.GetRequiredService<ModelBuilder>();

            foreach(var builder in provider.GetServices<IEntityBuilder>())
            {
                builder.Build();
            }
        }

        private static void AddEntityBuilders(IServiceCollection serviceCollection) =>
            serviceCollection
                .AddTransient<IEntityBuilder, EventEntityBuilder>()
                .AddTransient<IEntityBuilder, PhotoDataEntityBuilder>()
                .AddTransient<IEntityBuilder, PhotoDisplayPropertiesEntityBuilder>()
                .AddTransient<IEntityBuilder, PhotoGroupEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationChildEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationContactInformationEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationParentEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationPaymentInformationEntityBuilder>()
                .AddTransient<IEntityBuilder, UserEntityBuilder>();
    }
}