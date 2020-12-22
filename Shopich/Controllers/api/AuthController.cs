﻿using System;
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

namespace Shopich.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ShopichContext _context;

        public AuthController(ShopichContext context)
        {
            _context = context;
        }

        [HttpPost("api/v2/auth/login")]
        public IActionResult Login(LoginModel user)
        {
            var identity = GetIdentity(user.Email, user.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: jwtOptions.ISSUER,
                    audience: jwtOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(jwtOptions.LIFETIME + (user.RememberMe ? 10 : 0))),
                    signingCredentials: new SigningCredentials(jwtOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                email = identity.Name
            };

            return Json(response);
        }

        [HttpPost("api/v2/auth/register")]
        public async Task<IActionResult> Register(RegisterModel user)
        {
            var old_user = _context.Users.FirstOrDefault(u => u.UserEmail == user.email);
            if (old_user != null)
            {
                return BadRequest(new { errorText = "Email is already in use." });
            }

            await _context.Users.AddAsync(new User(user));
            await _context.SaveChangesAsync();

            return CreatedAtAction("User", user);
        }

        private ClaimsIdentity GetIdentity(string email, string password)
        {
            User person = _context.Users.Include(u => u.Role).FirstOrDefault(x => x.UserEmail == email && x.UserPassword == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserEmail),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.RoleName)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}