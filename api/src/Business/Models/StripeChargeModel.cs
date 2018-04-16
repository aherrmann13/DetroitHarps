namespace Business.Models
{
    public class StripeChargeModel
    {
        public string Email { get; set; }

        public string Token { get; set; }
        
        public int Amount { get; set; }

        public string Description { get; set; }
    }
}