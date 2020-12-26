using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Shopich.Models;
using Shopich.Config;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopich.ViewModels;
using Shopich.Repositories.interfaces;
using Shopich.Business_logic;
using Microsoft.AspNetCore.Http;

namespace Shopich.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUser _userRepository;

        public AuthController(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Login to get JWT token
        /// </summary>
        /// <param name="user"></param>
        /// <returns>JWT Token and email</returns>
        /// <response code="200">Success login</response>
        /// <response code="400">Wrong email or password</response>
        [HttpPost("api/v1/auth/login")]
        public IActionResult Login(LoginModel user)
        {
            User person = _userRepository.Include(u => u.Role).FirstOrDefault(x => x.UserEmail == user.Email && x.UserPassword == user.Password);
            if (person != null)
            {
                var identity = AuthLogic.GetIdentity(person);
                if (identity == null)
                {
                    return BadRequest(new { errorText = "Invalid email or password." });
                }

                var response = new
                {
                    access_token = AuthLogic.GenerateToken(identity, user.RememberMe),
                    email = identity.Name
                };

                return Json(response);
            }

            return BadRequest(new { errorText = "Invalid email or password." });
        }

        /// <summary>
        /// Register to shopich
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User data</returns>
        /// <response code="201">Success login</response>
        /// <response code="400">Email is already in use</response>
        [HttpPost("api/v1/auth/register")]
        public IActionResult Register(RegisterModel user)
        {
            var old_user = _userRepository.GetByEmail(user.email);
            if (old_user != null)
            {
                return BadRequest(new { errorText = "Email is already in use." });
            }

            _userRepository.Create(new User(user));
            _userRepository.Save();

            return CreatedAtAction("User", user);
        }
    }
}