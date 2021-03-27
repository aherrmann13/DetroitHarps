namespace DetroitHarps.Business
{
    public interface IHasId<I>
    {
        I Id { get; set; }
    }
}