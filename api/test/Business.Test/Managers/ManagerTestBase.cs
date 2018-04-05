namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Business.Interfaces;
    using Business.Managers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using Repository;
    using Xunit;

    public abstract class ManagerTestBase
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected ApiDbContext DbContext { get; set; }

        public ManagerTestBase()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApiDbContext>(
                builder => 
                {
                    builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                    builder.EnableSensitiveDataLogging();
                    builder.ConfigureWarnings(warning =>
                    {
                        warning.Throw(CoreEventId.IncludeIgnoredWarning);
                    });
                },
                ServiceLifetime.Transient, 
                ServiceLifetime.Scoped);

            serviceCollection.AddTransient<IScheduleManager, ScheduleManager>();
            serviceCollection.AddTransient<IRegistrationManager, RegistrationManager>();
            serviceCollection.AddTransient<IPhotoManager, PhotoManager>();

            ServiceProvider = serviceCollection.BuildServiceProvider();

            DbContext = ServiceProvider.GetRequiredService<ApiDbContext>();
        }
    }
}
