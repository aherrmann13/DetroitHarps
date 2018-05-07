namespace Service.Helpers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.IdentityModel.Tokens;
    using Service.Models;
    using Tools;

    public class JwtTokenFactory
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly SigningCredentials _signingCredentials;

        public JwtTokenFactory(JwtTokenOptions jwtTokenOptions)
        {
            Guard.NotNull(jwtTokenOptions, nameof(jwtTokenOptions));
            
            _jwtTokenOptions = jwtTokenOptions;
            _signingCredentials = GetSigningCredentials(jwtTokenOptions);
        }

        public string GenerateToken(int userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            };
            
            var jwt = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(_jwtTokenOptions.ValidFor),
                signingCredentials: _signingCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private static SigningCredentials GetSigningCredentials(JwtTokenOptions jwtTokenOptions)
        {
            Guard.NotNullOrWhiteSpace(jwtTokenOptions.Secret, nameof(jwtTokenOptions.Secret));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenOptions.Secret));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
    }
}