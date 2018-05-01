namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IScheduleManager
    {
        int Create(EventCreateModel models);

        int Update(EventUpdateModel models);

        int Delete(int id);

        IEnumerable<EventReadModel> GetAll();

        EventReadModel Get(int id);


    }
}