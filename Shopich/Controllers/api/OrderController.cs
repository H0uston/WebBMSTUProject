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
    [Route("api/v1/order")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;

        public OrderController(IOrder orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var orders = await _orderRepository.GetAll();
            return orders.Skip((current - 1) * size).Take(size);
        }

        [Route("{id:int}")]
        public async Task<Order> GetOrder(int id)
        {
            var order = await _orderRepository.GetById(id);
            return order;
        }

        [HttpPost]
        public async Task<Order> CreateOrder(int id)
        {
            var order = await _orderRepository.GetById(id);

            order.IsApproved = true;
            _orderRepository.Update(order);
            _orderRepository.Save();

            return order;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            _orderRepository.Delete(id);
            _orderRepository.Save();
            return NoContent();
        }
    }
}
