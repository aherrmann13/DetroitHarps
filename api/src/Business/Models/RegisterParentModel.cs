namespace Business.Models
{
    using System;

    public class RegisterParentModel : IParentModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}