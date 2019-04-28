namespace DetroitHarps.Business.Registration
{
    using DetroitHarps.Business.Registration.Entities;

    public interface IEventSnapshotProvider
    {
        RegistrationChildEventSnapshot GetSnapshot(int eventId);
    }
}