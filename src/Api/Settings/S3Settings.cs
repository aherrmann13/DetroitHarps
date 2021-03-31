namespace DetroitHarps.Api.Settings
{
    public class S3Settings
    {
        public const string SectionName = "S3";

        public string Url { get; set; }

        public string BucketName { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }
    }
}