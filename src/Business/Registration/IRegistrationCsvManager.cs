namespace DetroitHarps.Business.Registration
{
    public interface IRegistrationCsvManager
    {
        byte[] GetRegisteredParents(int year);

        byte[] GetRegisteredChildren(int year);
    }
}