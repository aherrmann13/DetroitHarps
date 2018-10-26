namespace DetroitHarps.DataAccess
{
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DetroitHarpsDbContext : DbContext
    {
        public DetroitHarpsDbContext(DbContextOptions<DetroitHarpsDbContext> options)
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

            foreach (var builder in provider.GetServices<IEntityBuilder>())
            {
                builder.Build();
            }
        }

        private static void AddEntityBuilders(IServiceCollection serviceCollection) =>
            serviceCollection
                .AddTransient<IEntityBuilder, EventEntityBuilder>()
                .AddTransient<IEntityBuilder, MessageEntityBuilder>()
                .AddTransient<IEntityBuilder, PhotoEntityBuilder>()
                .AddTransient<IEntityBuilder, PhotoGroupEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationChildEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationEntityBuilder>();
    }
}