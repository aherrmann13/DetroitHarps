namespace DetroitHarps.Business.Registration.Models
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegisterChildModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string ShirtSize { get; set; }

        public IList<RegisterChildEventModel> Events { get; set; }
    }
}