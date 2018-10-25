namespace DetroitHarps.Business.Contact
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Contact.Models;

    public interface IContactManager
    {
        void Contact(MessageModel model);

        void MarkAsRead(int id);

        void MarkAsUnread(int id);

        IEnumerable<MessageReadModel> GetAll();
    }
}