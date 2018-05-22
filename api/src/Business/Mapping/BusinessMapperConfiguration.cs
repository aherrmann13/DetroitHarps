namespace Business.Mapping
{
    using AutoMapper;

    public static class BusinessMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<EventProfile>();
            cfg.AddProfile<PhotoGroupProfile>();
            cfg.AddProfile<PhotoProfile>();
            cfg.AddProfile<RegistrationProfile>();
        }
    }
}