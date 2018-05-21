namespace Business.Test.Utility
{
    using Xunit;

    [CollectionDefinition("AutoMapper")]
    public class DatabaseCollection : ICollectionFixture<AutomapperFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}