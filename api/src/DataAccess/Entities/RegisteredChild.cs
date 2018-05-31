namespace DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataAccess.Interfaces;

    public class RegisteredChild : IHasId
    {
        [Key]
        public int Id { get; set; }

        public int RegisteredPersonId { get; set; }

        public RegisteredPerson RegisteredPerson { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ShirtSize { get; set; }
    }
}