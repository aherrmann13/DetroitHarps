namespace DetroitHarps.Business.Registration.Entities
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegistrationChild : IHasId, IHasDisable
    {
        // TODO: is there a way to eliminate this id?
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ShirtSize { get; set; }

        public IList<RegistrationChildEvent> Events { get; set; }

        public bool IsDisabled { get; set; }
    }
}