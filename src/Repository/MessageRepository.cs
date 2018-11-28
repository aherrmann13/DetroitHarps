namespace DetroitHarps.Repository
{
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.DataAccess;

    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(DetroitHarpsDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}