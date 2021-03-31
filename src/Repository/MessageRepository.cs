namespace DetroitHarps.Repository
{
    using System;
    using DetroitHarps.Business.Contact;
    using DetroitHarps.Business.Contact.Entities;
    using DetroitHarps.DataAccess.S3;

    public class MessageRepository : S3RepositoryBase<Message, Guid>, IMessageRepository
    {
        public MessageRepository(IS3ObjectStore<Message, Guid> messageStore)
            : base(messageStore)
        {
        }
    }
}