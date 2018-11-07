namespace DetroitHarps.DataAccess
{
    using Microsoft.EntityFrameworkCore;

    public interface IAuditPropertyManager
    {
        // TODO: dont love the type of param here, should maybe be a list of changed entities or something
        // im leaving it with the justification "this was easy to unit test" for now
        void SetTimestamps(DbContext dbContext);
    }
}