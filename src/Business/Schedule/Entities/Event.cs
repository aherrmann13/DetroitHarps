namespace DetroitHarps.Business.Schedule.Entities
{
    using System;

    public class Event : IHasId
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool CanRegister { get; set; }
    }
}