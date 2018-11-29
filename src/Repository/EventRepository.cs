namespace DetroitHarps.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using DetroitHarps.Business;
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Entities;
    using DetroitHarps.DataAccess;

    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}