namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Business.Interfaces;
    using Business.Managers;
    using Business.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Repository;
    using Xunit;

    public abstract class ManagerTestBase
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected Mock<IStripeManager> StripeManagerMock { get; set;}

        protected Mock<IContactManager> ContactManagerMock { get; set;}

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
            serviceCollection.AddTransient<IPhotoGroupManager, PhotoGroupManager>();

            StripeManagerMock = new Mock<IStripeManager>();
            StripeManagerMock.Setup(x => x.Charge(It.IsAny<StripeChargeModel>()))
                .Returns(Guid.NewGuid().ToString);

            ContactManagerMock = new Mock<IContactManager>();
            ContactManagerMock.Setup(x => x.Contact(It.IsAny<string>(), It.IsAny<string>()));

            serviceCollection.AddSingleton(StripeManagerMock.Object);
            serviceCollection.AddSingleton(ContactManagerMock.Object);


            ServiceProvider = serviceCollection.BuildServiceProvider();

            DbContext = ServiceProvider.GetRequiredService<ApiDbContext>();
        }
    }
}