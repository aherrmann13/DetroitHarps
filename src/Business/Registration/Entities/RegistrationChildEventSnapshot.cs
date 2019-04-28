namespace DetroitHarps.Business.Registration.Entities
{
    using System;
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegistrationChildEventSnapshot
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool CanRegister { get; set; }
    }
}