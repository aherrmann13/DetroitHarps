namespace Business.Test.Utility
{
    using AutoMapper;
    using Business.Mapping;

    public class AutomapperFixture
    {
        public AutomapperFixture()
        {
            Mapper.Reset();
            Mapper.Initialize(BusinessMapperConfiguration.Configure);
            Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}