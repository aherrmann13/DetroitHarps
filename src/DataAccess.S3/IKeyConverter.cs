namespace DetroitHarps.DataAccess.S3
{
    public interface IKeyConverter<T>
    {
        string ToString(T key);

        T FromString(string key);
    }
}
