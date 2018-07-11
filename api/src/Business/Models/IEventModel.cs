namespace Business.Models
{
    using System;
    
    public interface IEventModel
    {
        DateTime Date { get; set; }

        string Title { get; set; }

        string Description { get; set; }
    }
} 