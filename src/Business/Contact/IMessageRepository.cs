namespace DetroitHarps.Business.Contact
{
    using DetroitHarps.Business.Contact.Entities;

    public interface IMessageRepository : IRepository<Message, int>, IQueryableRepository<Message, int>
    {
    }
}