using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using Shopich.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Controllers.api
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUser _repository;

        public UserController(IUser repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get info of current user
        /// </summary>
        /// <returns>User info</returns>
        /// <response code="200"></response>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<User> GetUser()
        {
            var user = await _repository.GetByEmail(User.Identity.Name);

            return user;
        }

        /// <summary>
        /// Edit info of current user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Changed user</returns>
        /// <response code="200"></response>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<User> EditUser(User user)
        {
            _repository.Update(user);
            await _repository.Save();

            return user;
        }
    }
}
