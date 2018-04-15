namespace Repository.Dataloader.DataUnit
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Repository.Entities;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    // https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-2.1
    public class UserDataUnit : DataUnitBase<User>
    {
        private readonly DataloaderOptions _options;

        public UserDataUnit(ApiDbContext dbContext, DataloaderOptions options)
            : base(dbContext)
        {
            _options = options;
        }

        protected override IEnumerable<User> Data => 
            new List<User>
            {
                GetUser(_options.Email, _options.Password)
            };

        private static string GetPasswordHash(string password, byte[] salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

        private static byte[] GetPasswordSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private User GetUser(string email, string password)
        {
            var salt = GetPasswordSalt();
            var passwordHash = GetPasswordHash(password, salt);

            return new User
            {
                Email = email,
                Password = passwordHash,
                PasswordSalt = salt
            };
        }
    }
}
