namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IRegistrationManager
    {
        int Register(RegistrationCreateModel model);

        // TODO : by season?
        IEnumerable<RegistrationReadModel> GetAll();

        IEnumerable<ChildInformationReadModel> GetAllChildren();

    }
}