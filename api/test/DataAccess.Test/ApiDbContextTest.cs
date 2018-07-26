namespace Repository.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using DataAccess;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Xunit;

    public class ApiDbContextTest
    {
        private readonly ApiDbContext _dbContext;

        public ApiDbContextTest()
        {
            _dbContext = InMemoryDbContextProvider.GetContext();
        }

        [Fact]
        public void CreateDb()
        {
            Assert.True(_dbContext.Database.EnsureCreated());
        }

        [Fact]
        public void EnsureAllEntitiesCreated()
        {
            _dbContext.Database.EnsureCreated();

            var types = _dbContext.Model.GetEntityTypes();

            Assert.Equal(10, types.Count());
        }
    }
}
