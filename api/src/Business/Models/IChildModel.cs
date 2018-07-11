namespace Business.Models
{
    using System;
    
    public interface IChildModel
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Gender { get; set; }

        DateTimeOffset DateOfBirth { get; set; }
        
        // TODO : Enum? 
        string ShirtSize { get; set; }
    }
}