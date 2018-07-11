namespace Business.Entities
{
    using System;
    
    public class RegistrationChild : IHasId
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ShirtSize { get; set; }
    }
}