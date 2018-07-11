namespace Business.Managers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IRegistrationManager
    {
        void Register(RegisterModel model);

        void AddChild(RegisterAddChildModel model);

        IEnumerable<RegisteredChildModel> GetAllRegisteredChildren();

    }
}