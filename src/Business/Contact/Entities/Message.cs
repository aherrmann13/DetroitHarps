namespace DetroitHarps.Business.Contact.Entities
{
    using System;

    public class Message : IHasId
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public bool IsRead { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Body { get; set; }
    }
}