namespace Business.Models
{
    using System;
    
    public interface IContactInformationModel
    {
        string Email { get; set; }

        string PhoneNumber { get; set; }

        string Address { get; set; }

        string Address2 { get; set; }

        string City { get; set; }

        string State { get; set; }

        string Zip { get; set; }
    }
}