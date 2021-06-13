namespace DetroitHarps.Business.Registration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DetroitHarps.Business.Registration.Models;

    public interface IRegistrationManager
    {
        void Register(RegisterModel model);

        void Delete(int id);

        void DeleteChild(int id, string firstName, string lastName);

        IEnumerable<RegisteredParentModel> GetRegisteredParents(int year);

        IEnumerable<RegisteredChildModel> GetRegisteredChildren(int year);
    }
}