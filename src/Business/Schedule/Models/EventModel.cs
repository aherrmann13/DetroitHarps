namespace DetroitHarps.Business.Schedule.Models
{
    using System;

    public class EventModel
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}