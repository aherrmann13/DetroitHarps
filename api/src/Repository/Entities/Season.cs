namespace Repository.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Repository.Interfaces;

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