namespace Business.Mapping
{
    using System;
    using System.Linq;
    using AutoMapper;
    using Business.Entities;
    using Business.Models;

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