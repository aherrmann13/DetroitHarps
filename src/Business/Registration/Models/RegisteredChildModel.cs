namespace DetroitHarps.Business.Registration.Models
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegisteredChildModel
    {
        public int RegistrationId { get; set; }

        public string ParentFirstName { get; set; }

        public string ParentLastName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string EmailAddress { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string ShirtSize { get; set; }

        public IList<RegisteredChildEventModel> Events { get; set; }
    }
}