namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using DetroitHarps.Business.Registration.Models;

    public interface IRegistrationCsvManager
    {
        byte[] GetAllRegisteredParents();

        byte[] GetAllRegisteredChildren();
    }
}