namespace DetroitHarps.DataAccess.S3
{
    public class StringKeyConverter : IKeyConverter<string>
    {
        public string ToString(string key) => key;

        public string FromString(string key) => key;
    }
}