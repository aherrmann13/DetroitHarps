namespace Business.Models
{
    using System;

    public class RegistrationReadModel : RegistrationModelBase
    {
        public int Id { get; set; }

        public bool HasPaid { get; set; }

        public DateTimeOffset RegistrationTimestamp { get; set; }
    }
}