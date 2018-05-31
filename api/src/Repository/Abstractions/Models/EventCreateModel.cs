namespace Repository.Abstractions.Models
{
    using System;
    
    public class EventCreateModel : IEventModel
    {        
        public DateTimeOffset Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}