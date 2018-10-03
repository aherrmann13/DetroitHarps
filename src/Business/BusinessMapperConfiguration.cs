namespace DetroitHarps.Business
{
    using AutoMapper;
    using DetroitHarps.Business.Photo;
    using DetroitHarps.Business.Registration;

    public static class BusinessMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<RegistrationProfile>();
            cfg.AddProfile<PhotoProfile>();
        }
    }
}