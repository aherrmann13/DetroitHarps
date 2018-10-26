namespace DetroitHarps.Business.Registration.Entities
{
    using System;

    public class RegistrationChild
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ShirtSize { get; set; }
    }
}