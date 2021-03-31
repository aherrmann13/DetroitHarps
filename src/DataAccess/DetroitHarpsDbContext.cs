namespace DetroitHarps.DataAccess
{
    using System;
    using DetroitHarps.DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public class DetroitHarpsDbContext : DbContext
    {
        public DetroitHarpsDbContext(DbContextOptions<DetroitHarpsDbContext> options)
            : base(options)
        {
        }

        public IAuditPropertyManager AuditPropertyManager { get; set; } = new AuditPropertyManager();

        public override int SaveChanges()
        {
            AuditPropertyManager.SetTimestamps(this);

            return base.SaveChanges();
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
                .AddTransient<IEntityBuilder, PhotoEntityBuilder>()
                .AddTransient<IEntityBuilder, PhotoGroupEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationChildEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationChildEventEntityBuilder>()
                .AddTransient<IEntityBuilder, RegistrationEntityBuilder>();
    }
}