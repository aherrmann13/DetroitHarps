namespace Repository.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RegisteredPerson
    {
        public RegisteredPerson()
        {
            PaymentDetails = new List<PaymentDetails>();
            Children = new List<RegisteredChild>();
        }

        [Key]
        public int Id { get; set; }

        public Season Season { get; set; }

        public int SeasonId { get; set;}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public IList<RegisteredChild> Children { get; set; }

        public IList<PaymentDetails> PaymentDetails { get; set; }
    }
}