using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Controllers.api
{
    [ApiController]
    [Route("api/v2/category")]
    public class CategoryController : Controller
    {
        private readonly ShopichContext _context;

        public CategoryController(ShopichContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var categories = await _context.Categories.ToArrayAsync();
            return categories.Skip((current -  1) * size).Take(size);
        }

        [Route("{id:int}")]
        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }
    }
}
