namespace Repository.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PaymentDetails
    {
        [Key]
        public int Id { get; set; }

        public int RegisteredPersonId { get; set; }

        public RegisteredPerson RegisteredPerson { get; set; }

        public DateTimeOffset PaymentTimestamp { get; set; }

        public string StripeCustomerId { get; set; }
        
        public double Amount { get; set; }

    }
}