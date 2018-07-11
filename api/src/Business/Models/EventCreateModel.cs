namespace Business.Models
{
    using System;

    public class EventCreateModel : IEventModel
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}