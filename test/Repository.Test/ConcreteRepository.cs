namespace DetroitHarps.Repository.Test
{
    using DetroitHarps.DataAccess;

    public class ConcreteRepository : RepositoryBase<TestEntity, int>
    {
        public ConcreteRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
