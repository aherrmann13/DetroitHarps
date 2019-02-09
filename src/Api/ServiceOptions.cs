namespace DetroitHarps.Api
{
    public class ServiceOptions
    {
        public const string SectionName = nameof(ServiceOptions);

        public string ServiceName { get; set; }

        public string LogFolder { get; set; }

        public string BindUrl { get; set; }

        public bool EnableConsole { get; set; }
    }
}