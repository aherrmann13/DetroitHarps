namespace Business.Managers
{
    using System;
    using System.Collections.Generic;
    using Business.Models;

    public interface IScheduleManager
    {
        int Create(EventCreateModel model);

        void Update(EventModel model);

        void Delete(int id);

        IEnumerable<EventModel> GetAll();

        IEnumerable<EventModel> GetUpcoming(DateTime? untilDate = null);
    }
}