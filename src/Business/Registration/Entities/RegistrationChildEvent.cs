namespace DetroitHarps.Business.Registration.Entities
{
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegistrationChildEvent : IHasId
    {
        // TODO: is there a way to eliminate this id?
        public int Id { get; set; }
        
        public Answer Answer { get; set; }

        public int EventId { get; set; }

        public RegistrationChildEventSnapshot EventSnapshot { get; set; }
    }
}