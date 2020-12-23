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
    [Route("api/v2/product")]
    public class ProductController : Controller
    {
        private readonly ShopichContext _context;

        public ProductController(ShopichContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var products = await _context.Products.ToArrayAsync();
            return products.Skip((current - 1) * size).Take(size);
        }

        [Route("{id:int}")]
        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }
    }
}
