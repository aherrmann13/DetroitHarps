namespace DetroitHarps.Business.Registration.Entities
{
    using DetroitHarps.Business.Registration.DataTypes;

    public class RegistrationPaymentInformation
    {
        public PaymentType PaymentType { get; set; }

        public double Amount { get; set; }
    }
}