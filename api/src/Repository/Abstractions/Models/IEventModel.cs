namespace Repository.Abstractions.Models
{
    using System;
    
    public interface IEventModel
    {
        DateTimeOffset Date { get; set; }

        string Title { get; set; }

        string Description { get; set; }
    }
}