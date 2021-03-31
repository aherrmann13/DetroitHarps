namespace DetroitHarps.Business.Contact
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Contact.Models;

    public interface IContactManager
    {
        void Contact(MessageModel model);

        void MarkAsRead(Guid id);

        void MarkAsUnread(Guid id);

        IEnumerable<MessageReadModel> GetAll();
    }
}