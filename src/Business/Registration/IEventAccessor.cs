namespace DetroitHarps.Business.Registration
{
    using DetroitHarps.Business.Registration.Entities;

    public interface IEventAccessor
    {
        RegistrationChildEventSnapshot GetSnapshot(int eventId);

        string GetName(int eventId);
    }
}