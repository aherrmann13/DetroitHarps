namespace DetroitHarps.Business.Contact
{
    using System;
    using System.Collections.Generic;

    public interface IMessageStatusRepository
    {
        IList<Guid> GetUnreadMessageIds();

        void SetAsRead(Guid messageId);

        void SetAsUnread(Guid messageId);
    }
}