namespace Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Event
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}