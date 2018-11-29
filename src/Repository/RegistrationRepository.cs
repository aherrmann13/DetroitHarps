namespace DetroitHarps.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using DetroitHarps.Business;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Registration.Entities;
    using DetroitHarps.DataAccess;
    using Microsoft.EntityFrameworkCore;

    public class RegistrationRepository : RepositoryBase<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }

        // TODO: maybe figure out how to unit test?
        protected override IQueryable<Registration> BaseQuery => 
            DbContext.Set<Registration>()
                .Include(x => x.Parent)
                .Include(x => x.ContactInformation)
                .Include(x => x.PaymentInformation)
                .Include(x => x.Children);
    }
}