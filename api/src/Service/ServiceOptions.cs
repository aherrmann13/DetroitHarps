namespace Service
{
    using System.Collections.Generic;
    
    public class ServiceOptions
    {
        public string ServiceName { get; set; }

        public string LogFolder { get; set; }

        public string BindUrl { get; set; }

        public IList<string> CorsAllowUrls { get; set; }
    }
}