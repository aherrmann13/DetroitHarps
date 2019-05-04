namespace DetroitHarps.Business.Registration.Models
{
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegisteredChildEventModel
    {
        public Answer Answer { get; set; }

        public int EventId { get; set; }

        public RegisteredChildEventSnapshotModel EventSnapshot { get; set; }
    }
}