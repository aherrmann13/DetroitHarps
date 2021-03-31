namespace DetroitHarps.Repository
{
    using DetroitHarps.Business.Schedule;
    using DetroitHarps.Business.Schedule.Entities;
    using DetroitHarps.DataAccess;

    public class EventRepository : DbContextRepositoryBase<Event, int>, IEventRepository
    {
        public EventRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}