namespace DataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DataAccess.Interfaces;

    public class PaymentDetails: IHasId
    {
        [Key]
        public int Id { get; set; }

        public int RegisteredPersonId { get; set; }

        public RegisteredPerson RegisteredPerson { get; set; }

        public DateTimeOffset PaymentTimestamp { get; set; }

        public string StripeCustomerId { get; set; }

        public string PaymentType { get; set; }

        public bool VerfiedPayment { get; set; }
        
        public double Amount { get; set; }

    }
}