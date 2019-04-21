namespace DetroitHarps.Business.Schedule.Models
{
    using System;

    public class EventCreateModel
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool CanRegister { get; set; }
    }
}