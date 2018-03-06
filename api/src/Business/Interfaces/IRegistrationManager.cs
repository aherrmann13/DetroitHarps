namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IRegistrationManager
    {
        IEnumerable<int> Register(params RegistrationCreateModel[] models);

        // TODO : by season?
        IEnumerable<RegistrationReadModel> GetRegisteredPersons();

        IEnumerable<RegistrationReadModel> GetPaidPersons();

        IEnumerable<RegistrationReadModel> GetUnpaidPersons();

        IEnumerable<ChildInformationReadModel> GetRegisteredChildren();

    }
}