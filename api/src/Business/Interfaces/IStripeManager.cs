namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IStripeManager
    {
        string Charge(StripeChargeModel model);

    }
}