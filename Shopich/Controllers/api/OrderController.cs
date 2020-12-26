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
    [Route("api/v1/order")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;

        public OrderController(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <param name="current"></param>
        /// <param name="size"></param>
        /// <returns>List of orders</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var orders = await _orderRepository.GetAll();

            return orders.Skip((current - 1) * size).Take(size);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order object</returns>
        /// <response code="200"></response>
        [HttpGet("{id:int}")]
        public async Task<Order> GetOrder(int id)
        {
            var order = await _orderRepository.GetById(id);

            return order;
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Approved order</returns>
        /// <response code="201"></response>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(int id)
        {
            var order = await _orderRepository.GetById(id);

            order = OrderLogic.ApproveOrder(order);

            _orderRepository.Update(order);
            _orderRepository.Save();

            return CreatedAtAction("order", order);
        }


        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code</returns>
        /// <response code="204"></response>
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
            _orderRepository.Save();

            return NoContent();
        }
    }
}
