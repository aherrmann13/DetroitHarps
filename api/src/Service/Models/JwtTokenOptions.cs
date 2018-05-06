using System;

namespace Service.Models
{
    public class JwtTokenOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Secret { get; set; }

        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
    }
}