namespace Repository.Dataloader
{
    public class DataloaderOptions
    {
        public const string SectionName = "DataloaderOptions";

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConnectionString { get; set; }

        public string LogFolder { get; set; }
    }
}