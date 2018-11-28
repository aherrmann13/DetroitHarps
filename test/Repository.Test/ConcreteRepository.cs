namespace DetroitHarps.Repository.Test
{
    using DetroitHarps.DataAccess;

    public class ConcreteRepository : RepositoryBase<TestEntity>
    {
        public ConcreteRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
