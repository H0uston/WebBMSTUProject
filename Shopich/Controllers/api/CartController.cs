using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CartController : Controller
    {
        private readonly IOrders _ordersRepository;
        private readonly IOrder _orderRepository;
        private readonly IUser _userRepository;
        private readonly IProduct _productRepository;

        public CartController(IOrders ordersRepository, IOrder orderRepository, IUser userRepository, IProduct productRepository)
        {
            _ordersRepository = ordersRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get products form cart
        /// </summary>
        /// <param name="current">Current page</param>
        /// <param name="size">Size of page</param>
        /// <returns>Products from cart</returns>
        /// <response code="200">Array of products</response>
        [HttpGet]
        public async Task<IEnumerable<Orders>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            var productsInCart = await _ordersRepository.GetProductsInCart(order.OrderId);

            return productsInCart.Skip((current - 1) * size).Take(size);
        }

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="productId">id of product</param>
        /// <param name="count">count of product</param>
        /// <returns>Added product and its count</returns>
        /// <response code="200">Added product and its count</response>
        /// <response code="400">Invalid count of product</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductToCart(int productId, int count)
        {
            if (count <= 0)
            {
                return BadRequest("Count must be positive");
            }

            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);

            if (order == null)
            {
                order = CartLogic.CreateUnacceptedOrder(currentUser);
                await _orderRepository.Create(order);
                await _ordersRepository.Save();
                order = await _orderRepository.GetUnacceptedOrder(currentUser.UserId);
            }

            var orders = CartLogic.AddProductToCart(order, await _productRepository.GetById(productId), count);
            await _ordersRepository.Create(orders);
            await _ordersRepository.Save();

            return Json(orders);
        }

        /// <summary>
        /// Change count of product
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>Status code</returns>
        /// <response code="200">Added product and its count</response>
        /// <response code="400">Invalid count of product</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeCount(Orders orders)
        {
            if (orders.Count <= 0)
            {
                return BadRequest("Count must be positive");
            }
            var newOrders = await _ordersRepository.GetById(orders.OrdersId);
            newOrders = OrderLogic.UpdateCount(newOrders, orders.Count);

            _ordersRepository.Update(newOrders);
            await _ordersRepository.Save();

            return Ok();
        }

        /// <summary>
        /// Delete product from cart
        /// </summary>
        /// <param name="ordersId"></param>
        /// <returns>Status code</returns>
        /// <response code="204">Product was deleted</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int ordersId)
        {
            var orders = await _ordersRepository.Include(o => o.OrderNavigation).FirstOrDefaultAsync(o => o.OrdersId == ordersId);
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);

            if (orders == null)
            {
                return BadRequest("No such order");
            }
            else if (orders.OrderNavigation.UserId != currentUser.UserId)
            {
                return BadRequest("Try to delete another users product");
            }
            else
            {
                await _ordersRepository.Delete(ordersId);
                await _ordersRepository.Save();
            }

            return NoContent();
        }
    }
}
