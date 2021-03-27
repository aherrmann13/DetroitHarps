namespace DetroitHarps.Business.Schedule
{
    using DetroitHarps.Business.Schedule.Entities;

    public interface IEventRepository : IRepository<Event, int>, IQueryableRepository<Event, int>
    {
    }
}