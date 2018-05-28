namespace Business.Mapping
{
    using AutoMapper;

    public static class BusinessMapperConfiguration
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<PhotoGroupProfile>();
            cfg.AddProfile<PhotoProfile>();
        }
    }
}