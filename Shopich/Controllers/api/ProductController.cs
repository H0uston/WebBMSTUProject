using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopich.Business_logic;
using Shopich.Models;
using Shopich.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Controllers.api
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : Controller
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get list of products
        /// </summary>
        /// <param name="productName">Name of product</param>
        /// <param name="current">Current page</param>
        /// <param name="size">Size of page</param>
        /// <returns>List of products</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll([FromQuery] string productName = null, [FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            IEnumerable<Product> products = null;
            if (productName != null)
            {
                products = await _productRepository.GetByName(productName);
            }
            else
            {
                products = await _productRepository.GetAll();
            }

            products = ProductLogic.SetRating(products);
            products = ProductLogic.SetDiscontPrice(products);
            
            return products.Skip((current - 1) * size).Take(size);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>Product object</returns>
        /// <response code="200"></response>
        [HttpGet("{id:int}")]
        public async Task<Product> GetProduct(int id)
        {
            var product = await _productRepository.GetById(id);

            if (product != null)
            {
                product.SetProductRating();
                product.SetPriceWithDiscount();
            }
            
            return product;
        }
    }
}
