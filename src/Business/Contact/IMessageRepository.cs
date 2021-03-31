namespace DetroitHarps.Business.Contact
{
    using System;
    using DetroitHarps.Business.Contact.Entities;

    public interface IMessageRepository : IRepository<Message, Guid>
    {
    }
}