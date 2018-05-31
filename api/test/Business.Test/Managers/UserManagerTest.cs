namespace Business.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using DataAccess;
    using DataAccess.Entities;
    using Tools;
    using Xunit;

    public class UserManagerTest : ManagerTestBase
    {
        private readonly IUserManager _manager;

        private readonly string _existingUserPassword;
        private readonly User _existingUser;

        public UserManagerTest() : base()
        {
            _manager = ServiceProvider.GetRequiredService<IUserManager>();
            _existingUserPassword = Guid.NewGuid().ToString();
            _existingUser = SeedUser(_existingUserPassword);
        }

        [Fact]
        public void GetUserIdSuccessTest()
        {
            var model = GetValidCredentialsModel();

            var response = _manager.GetUserId(model);

            Assert.Equal(_existingUser.Id, response);
        }

        [Fact]
        public void GetUserIdWrongPasswordTest()
        {
            var model = GetValidCredentialsModel();
            model.Password = Guid.NewGuid().ToString();

            var response = _manager.GetUserId(model);

            Assert.Null(response);
        }

        [Fact]
        public void GetUserIdWrongEmailTest()
        {
            var model = GetValidCredentialsModel();
            model.Email = Guid.NewGuid().ToString();

            var response = _manager.GetUserId(model);

            Assert.Null(response);
        }

        [Fact]
        public void GetUserIdNullModelThrowsExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => _manager.GetUserId(null));
        }

        private UserCredentialsModel GetValidCredentialsModel() =>
            new UserCredentialsModel
            {
                Email = _existingUser.Email,
                Password = _existingUserPassword
            };

        private User SeedUser(string password)
        {
            var salt = GetPasswordSalt();
            var hash = GetPasswordHash(password, salt);

            var user = new User
            {
                Email = Guid.NewGuid().ToString(),
                PasswordSalt = salt,
                Password = hash
            };

            DbContext.Add(user);
            DbContext.SaveChanges();

            return user;
        }

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
    }
}