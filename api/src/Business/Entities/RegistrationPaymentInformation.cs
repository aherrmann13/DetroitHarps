namespace Business.Entities
{
    using System;

    public class RegistrationPaymentInformation: IHasId
    {
        public int Id { get; set; }

        public DateTimeOffset PaymentTimestamp { get; set; }

        public string PaymentType { get; set; }
        
        public double Amount { get; set; }

    }
}