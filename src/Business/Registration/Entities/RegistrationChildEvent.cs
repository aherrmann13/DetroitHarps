namespace DetroitHarps.Business.Registration.Entities
{
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegistrationChildEvent
    {
        public Answer Answer { get; set; }

        public int EventId { get; set; }

        public RegistrationChildEventSnapshot EventSnapshot { get; set; }
    }
}