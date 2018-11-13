namespace DetroitHarps.Business.Registration.Models
{
    using System;
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegisteredParentModel
    {
        // TODO: would prefer this on an object for registration not the parent
        public int RegistrationId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int ChildCount { get; set; }
    }
}