namespace DetroitHarps.Repository.Internal
{
    using System;
    using System.Collections.Generic;
    using DetroitHarps.Business;

    public class MessageStatusContainer : IHasId<string>
    {
        public string Id { get; set; }

        public ISet<Guid> UnreadMessages { get; set; }
    }
}