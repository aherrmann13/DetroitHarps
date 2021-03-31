namespace DetroitHarps.DataAccess.S3
{
    public class IntKeyConverter : IKeyConverter<int>
    {
        public string ToString(int key) => key.ToString();

        public int FromString(string key) => int.Parse(key);
    }
}