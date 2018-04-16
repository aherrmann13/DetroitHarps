namespace Business.Models
{
    using System;
    
    public abstract class EventModelBase
    {
        public DateTimeOffset Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}