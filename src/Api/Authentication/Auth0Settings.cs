namespace DetroitHarps.Api.Authentication
{
    public class Auth0Settings
    {
        public const string SectionName = "Auth0";

        public string Domain { get; set; }

        public string Authority { get; set; }

        public string ApiIdentifier { get; set; }
    }
}