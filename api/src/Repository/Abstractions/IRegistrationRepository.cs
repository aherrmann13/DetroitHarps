namespace Repository.Abstractions
{
    using System.Collections.Generic;
    using Repository.Abstractions.Models;

    public interface IRegistrationRepository
    {
        int Create(RegistrationCreateModel model);

        void Update(RegistrationUpdateModel model);

        void Delete(int id);

        IEnumerable<RegistrationReadModel> GetAll();

        IEnumerable<RegistrationReadModel> Get(int id);
    }
}