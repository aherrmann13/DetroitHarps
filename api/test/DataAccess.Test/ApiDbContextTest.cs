namespace DataAccess.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using DataAccess;
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
    }
}
