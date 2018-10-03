namespace DetroitHarps.Business.Registration.Models
{
    using System;

    public class RegisterContactInformationModel
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}