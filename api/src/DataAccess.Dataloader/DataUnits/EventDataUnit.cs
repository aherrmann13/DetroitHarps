namespace DataAccess.Dataloader.DataUnit
{
    using System;
    using System.Collections.Generic;
    using DataAccess.Entities;

    public class EventDataUnit : DataUnitBase<Event>
    {
        public EventDataUnit(ApiDbContext dbContext)
            : base(dbContext)
        {
        }

        protected override IEnumerable<Event> Data => 
            new List<Event>
            {
                new Event
                {
                    Date = new DateTime(2018, 06, 10),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 06, 16),
                    Title = "Motor City Irish Festival - Exhibition Games",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 06, 24),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 07, 01),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 07, 08),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 07, 15),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 07, 22),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 07, 29),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 08, 05),
                    Title = "Summer Program Canton 5:00 - 6:30pm",
                    Description = ""
                },
                new Event
                {
                    Date = new DateTime(2018, 08, 11),
                    Title = "Midwest Tournament Buffalo, NY",
                    Description = ""
                }
            };
    }
}
