namespace Business.Models
{
    using System;
    
    public abstract class ChildInformationModelBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }
        
        // TODO : Enum? 
        public string ShirtSize { get; set; }
    }
}