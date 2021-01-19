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
        /// <returns>List of categories</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<Category[]> GetAll()
        {
            var categories = await _categoryRepository.GetAll();

            return categories;
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Category object</returns>
        /// <response code="200"></response>
        [HttpGet("{categoryId}")]
        public async Task<Category> GetCategory(int categoryId)
        {
            var category = await _categoryRepository.GetById(categoryId);

            return category;
        }
    }
}
