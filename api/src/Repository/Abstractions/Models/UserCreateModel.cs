namespace Repository.Abstractions.Models
{
    using System;
    
    public class UserCreateModel : IUserModel
    {        
        public string Email { get; set; }

        public string Password { get; set; }
    }
}