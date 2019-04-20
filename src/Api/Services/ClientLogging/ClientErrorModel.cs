namespace DetroitHarps.Api.Services.ClientLogging
{
    using System;

    public class ClientErrorModel
    {
        public DateTime Timestamp { get; set; }

        public string SessionId { get; set; }

        public string Message { get; set; }
    }
}