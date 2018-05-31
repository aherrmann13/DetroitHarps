namespace Repository.Abstractions
{
    using System.Collections.Generic;
    using Repository.Abstractions.Models;
    
    public interface IEventRepository
    {
        int Create(EventCreateModel model);

        void Update(EventUpdateModel model);

        void Delete(int id);

        IEnumerable<EventReadModel> GetAll();

        IEnumerable<EventReadModel> Get(int id);
    }
}