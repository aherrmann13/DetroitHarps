namespace DetroitHarps.Business.Schedule
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business.Schedule.Models;

    public interface IScheduleManager
    {
        int Create(EventCreateModel model);

        void Update(EventModel model);

        void Delete(int id);

        IEnumerable<EventModel> GetAll(int? year = null);

        IEnumerable<EventModel> GetUpcoming(DateTime? untilDate = null);

        IEnumerable<EventModel> GetUpcomingRegistrationEvents();
    }
}