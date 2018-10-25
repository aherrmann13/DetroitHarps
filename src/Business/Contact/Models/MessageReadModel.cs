namespace DetroitHarps.Business.Contact.Models
{
    using System;

    public class MessageReadModel : MessageModel
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public bool IsRead { get; set; }
    }
}