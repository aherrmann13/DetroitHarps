namespace DetroitHarps.DataAccess.S3
{
    using System;

    public class GuidKeyConverter : IKeyConverter<Guid>
    {
        public string ToString(Guid key) => key.ToString();

        public Guid FromString(string key) => Guid.Parse(key);
    }
}