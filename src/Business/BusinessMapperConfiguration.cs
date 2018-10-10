namespace DetroitHarps.Business
{
    using AutoMapper;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Registration;
    using DetroitHarps.Business.Schedule;

    public static class BusinessMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<RegistrationProfile>();
            cfg.AddProfile<PhotoProfile>();
            cfg.AddProfile<PhotoGroupProfile>();
            cfg.AddProfile<EventProfile>();
        }
    }
}