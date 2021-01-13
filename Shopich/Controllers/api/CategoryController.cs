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
    [Route("api/v1/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepository;

        public CategoryController(ICategory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <param name="current"></param>
        /// <param name="size"></param>
        /// <returns>List of categories</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<Category[]> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var categories = await _categoryRepository.GetAll();

            return categories.Skip((current -  1) * size).Take(size).ToArray();
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category object</returns>
        /// <response code="200"></response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryRepository.GetById(id);

            return Json(category);
        }
    }
}
