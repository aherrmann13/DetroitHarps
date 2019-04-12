namespace DetroitHarps.Api.Services
{
    public class EmailSettings
    {
        public const string SectionName = nameof(EmailSettings);

        public string Email { get; set; }

        public string Password { get; set; }

        public string RecievingEmail { get; set; }
    }
}