namespace DetroitHarps.Repository.Test
{
    using DetroitHarps.Business;

    public class TestEntity : IHasId<int>
    {
        public int Id { get; set; }
    }
}
