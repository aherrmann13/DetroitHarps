namespace Business.Models
{
    using System;

    public class RegisterAddChildModel : IChildModel
    {
        public string ParentFirstName { get; set; }

        public string ParentLastName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
        
        public string ShirtSize { get; set; }
    }
}