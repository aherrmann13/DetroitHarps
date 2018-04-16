namespace Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Business.Models;

    public interface IScheduleManager
    {
        IEnumerable<int> Create(params EventCreateModel[] models);

        IEnumerable<int> Update(params EventUpdateModel[] models);

        IEnumerable<int> Delete(params int[] ids);

        IEnumerable<EventReadModel> GetAll();

        IEnumerable<EventReadModel> Get(params int[] ids);


    }
}