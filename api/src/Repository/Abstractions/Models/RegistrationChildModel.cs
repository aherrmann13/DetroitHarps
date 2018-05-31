namespace Repository.Abstractions.Models
{
    using System;
    
    public class RegistrationChildModel
    {        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
         
        public string ShirtSize { get; set; }
    }
}