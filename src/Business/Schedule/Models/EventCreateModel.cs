namespace DetroitHarps.Business.Schedule.Models
{
    using System;

    public class EventCreateModel
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}