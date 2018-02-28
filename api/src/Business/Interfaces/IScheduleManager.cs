namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IScheduleManager
    {
        IEnumerable<int> CreateEvent(params EventCreateModel[] models);

        IEnumerable<int> UpdateEvent(params EventUpdateModel[] models);

        IEnumerable<int> DeleteEvent(params int[] ids);

        IEnumerable<EventReadModel> GetAllEvents();

        IEnumerable<EventReadModel> GetEvents(params int[] ids);


    }
}