namespace Business.Managers
{
    using Business.Interfaces;
    using Business.Models;
    using Stripe;
    using Tools;

    public class StripeManager : IStripeManager
    {
        public string Charge(StripeChargeModel model)
        {
            Guard.NotNull(model, nameof(model));
            
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions {
                Email = model.Email,
                SourceToken = model.Token
            });

            var charge = charges.Create(new StripeChargeCreateOptions {
                Amount = model.Amount,
                Description = model.Description,
                Currency = "usd",
                CustomerId = customer.Id
            });

            return customer.Id;
        }
    }
}