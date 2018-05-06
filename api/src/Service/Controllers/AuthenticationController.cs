namespace Service.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Business.Interfaces;
    using Business.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Service.Helpers;
    using Service.Models;
    using Tools;
    

    [Route("[Controller]")]
    public class AuthenticationController : Controller
    {
        private readonly JwtTokenFactory _jwtTokenFactory;
        private readonly IUserManager _userManager;

        public AuthenticationController(
            IUserManager userManager,
            JwtTokenFactory jwtTokenFactory)
        {
            Guard.NotNull(userManager, nameof(userManager));
            Guard.NotNull(jwtTokenFactory, nameof(jwtTokenFactory));

            _userManager = userManager;
            _jwtTokenFactory = jwtTokenFactory;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserCredentialsModel userCredentials)
        {
            var userId = _userManager.GetUserId(userCredentials);

            if(!userId.HasValue)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(_jwtTokenFactory.GenerateToken(userId.Value));
            }
        }
    }
}