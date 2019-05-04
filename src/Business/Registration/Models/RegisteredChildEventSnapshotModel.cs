namespace DetroitHarps.Business.Registration.Models
{
    using System;

    public class RegisteredChildEventSnapshotModel
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}