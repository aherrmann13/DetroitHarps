namespace Repository.Abstractions.Models
{
    using System;
    using System.Collections.Generic;

    public interface IRegistrationModel
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string EmailAddress { get; set; }

        string PhoneNumber { get; set; }

        string Address { get; set; }

        string Address2 { get; set; }

        string City { get; set; }

        string State { get; set; }

        string Zip { get; set; }

        IList<RegistrationChildModel> Children { get; set; }
    }
}