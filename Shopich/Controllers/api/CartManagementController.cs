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
    [Route("api/v1/cart")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardManagementController : Controller
    {
        private readonly IOrders _ordersRepository;
        private readonly IOrder _orderRepository;
        private readonly IUser _userRepository;
        private readonly IProduct _productRepository;

        public CardManagementController(IOrders ordersRepository, IOrder orderRepository, IUser userRepository, IProduct productRepository)
        {
            _ordersRepository = ordersRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Orders>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            var productsInCart = await _ordersRepository.GetProductsInCart(order.OrderId);
            return productsInCart.Skip((current - 1) * size).Take(size);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int productId, int count)
        {
            if (count < 0)
            {
                return BadRequest("Count must be positive");
            }

            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);

            if (order == null)
            {
                order = CartLogic.CreateUnacceptedOrder(currentUser);
                _orderRepository.Create(order);
                _ordersRepository.Save();
                order = await _orderRepository.GetUnacceptedOrder(currentUser.UserId);
            }

            var orders = CartLogic.AddProductToCart(order, await _productRepository.GetById(productId), count);
            _ordersRepository.Create(orders);
            _ordersRepository.Save();

            return Json(orders);
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
