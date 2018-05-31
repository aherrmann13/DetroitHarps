namespace Repository.Abstractions.Models
{
    using System;
    using System.Collections.Generic;

    public class RegistrationCreateModel : IRegistrationModel
    {
        public RegistrationCreateModel()
        {
            Children = new List<RegistrationChildModel>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public IList<RegistrationChildModel> Children { get; set; }
    }
}