namespace DetroitHarps.Business.Registration.Entities
{
    using System;

    public class RegistrationPaymentInformation
    {
        public DateTimeOffset PaymentTimestamp { get; set; }

        public RegistrationPaymentType PaymentType { get; set; }

        public double Amount { get; set; }
    }
}