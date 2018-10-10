namespace DetroitHarps.Business.Schedule.Entities
{
    using System;

    public class Event : IHasId
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}