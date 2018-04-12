namespace Service
{
    public class ServiceOptions
    {
        public const string SectionName = "ServiceOptions";

        public string ServiceName { get; set; }

        public string LogFolder { get; set; }

        public string BindUrl { get; set;}
    }
}