namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DetroitHarps.Business.Registration.Models;

    public interface IRegistrationManager
    {
        void Register(RegisterModel model);

        void Delete(int id);

        IEnumerable<RegisteredParentModel> GetAllRegisteredParents();

        IEnumerable<RegisteredChildModel> GetAllRegisteredChildren();
    }
}