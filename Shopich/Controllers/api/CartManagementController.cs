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
    [Route("api/v1/cart")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardManagementController : Controller
    {
        private readonly IOrders _ordersRepository;
        private readonly IOrder _orderRepository;
        private readonly IUser _userRepository;

        public CardManagementController(IOrders ordersRepository, IOrder orderRepository, IUser userRepository)
        {
            _ordersRepository = ordersRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Orders>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            var productsInCart = await _ordersRepository.GetProductsInCart(order.OrderId);
            return productsInCart.Skip((current - 1) * size).Take(size);
        }

        [HttpPost]
        public async Task<Order> AddProductToCart(int productId)
        {
            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);

            if (order == null)
            {
                var now = DateTime.UtcNow;
                var user = await _userRepository.GetByEmail(User.Identity.Name);
                order = new Order();
                order.UserId = user.UserId;
                order.OrderDate = now;
                order.IsApproved = false;
                _orderRepository.Create(order);
            }
            // TODO
            var orders = new Orders();
            order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            orders.OrderId = order.OrderId;
            orders.ProductId = productId;
            _ordersRepository.Create(orders);
            _ordersRepository.Save();

            return order;
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeCount(Orders orders)
        {
            var newOrders = await _ordersRepository.GetById(orders.OrdersId);
            newOrders.Count = orders.Count;

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Orders orders)
        {
            _ordersRepository.Delete(orders.OrdersId);
            _ordersRepository.Save();
            return NoContent();
        }
    }
}
