namespace Business.Models
{
    using System;

    public class RegisteredChildModel : IChildModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
        
        public string ShirtSize { get; set; }
    }
}