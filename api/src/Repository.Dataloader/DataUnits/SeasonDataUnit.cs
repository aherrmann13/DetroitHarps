namespace Repository.Dataloader.DataUnit
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Repository.Entities;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public class SeasonDataUnit : DataUnitBase<Season>
    {
        private readonly DataloaderOptions _options;

        public SeasonDataUnit(ApiDbContext dbContext, DataloaderOptions options)
            : base(dbContext)
        {
            _options = options;
        }

        protected override IEnumerable<Season> Data => 
            new List<Season>
            {
                new Season
                {
                    Year = 2018
                }
            };
    }
}
