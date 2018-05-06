namespace Business.Managers
{
    using System;
    using System.Linq;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Repository.Entities;
    using Stripe;
    using Tools;

    public class UserManager : IUserManager
    {
        private readonly ApiDbContext _dbContext;

        public UserManager(ApiDbContext dbContext)
        {
            Guard.NotNull(dbContext, nameof(dbContext));

            _dbContext = dbContext;
        }
        
        public int? GetUserId(UserCredentialsModel model)
        {
            Guard.NotNull(model, nameof(model));

            var user = _dbContext.Set<User>()
                .AsNoTracking()
                .FirstOrDefault(x => x.Email == model.Email);

            if(user == null)
            {
                return null;
            }

            if(IsPasswordCorrect(model.Password, user.PasswordSalt, user.Password))
            {
                return user.Id;
            }
            else
            {
                return null;
            }
        }

        public bool IsPasswordCorrect(string password, byte[] salt, string hash)
        {
            var suppliedPasswordHash = GetPasswordHash(password, salt);

            return Compare.EqualOrdinal(suppliedPasswordHash, hash);
        }

        private static string GetPasswordHash(string password, byte[] salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
    }
}