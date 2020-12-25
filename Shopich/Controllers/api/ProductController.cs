﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly IProduct _productRepository;

        public ProductController(IProduct productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var products = await _productRepository.GetAll();

            return products.Skip((current - 1) * size).Take(size);
        }

        [Route("{id:int}")]
        public async Task<Product> GetProduct(int id)
        {
            var product = await _productRepository.GetById(id);

            return product;
        }
    }
}
