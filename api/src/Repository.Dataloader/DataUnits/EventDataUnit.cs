namespace Repository.Dataloader.DataUnit
{
    using System;
    using System.Collections.Generic;
    using Repository.Entities;

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
                    Date = new DateTime(2018, 06, 15),
                    Title = "Title 1",
                    Description = "This is a test"
                },
                new Event
                {
                    Date = new DateTime(2018, 07, 15),
                    Title = "Title 2",
                    Description = "This is a test"
                },
                new Event
                {
                    Date = new DateTime(2018, 08, 15),
                    Title = "Title 3",
                    Description = "This is a test"
                }
            };
    }
}
