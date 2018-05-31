namespace DataAccess.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataAccess.Interfaces;

    public class Season : IHasId
    {

        public Season()
        {
            RegisteredPeople = new List<RegisteredPerson>();
        }
        
        [Key]
        public int Id { get; set; }

        public int Year { get; set; }

        public IList<RegisteredPerson> RegisteredPeople { get; set; }
    }
}