namespace DetroitHarps.Business.Test.Utility
{
    using AutoMapper;
    using Xunit;

    public class AutomapperFixture
    {
        public AutomapperFixture()
        {
            Mapper.Reset();
            Mapper.Initialize(BusinessMapperConfiguration.Configure);
            Mapper.AssertConfigurationIsValid();
        }
    }
}