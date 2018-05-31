namespace Repository.Abstractions.Models
{
    using System;
    
    public class EventReadModel : IEventModel, IModelWithId
    {
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}