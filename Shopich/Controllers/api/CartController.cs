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
    [Route("api/v1/cart/products")]
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
        /// <returns>Products from cart</returns>
        /// <response code="200">Array of products</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Orders>> GetAll()
        {
            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);
            if (order == null)
            {
                IEnumerable<Orders> orders = Enumerable.Empty<Orders>();
                return orders;
            }
            var productsInCart = await _ordersRepository.GetProductsInCart(order.OrderId);

            return productsInCart;
        }

        /// <summary>
        /// Add product to cart
        /// </summary>
        /// <param name="orders">product in cart</param>
        /// <returns>Added product and its count</returns>
        /// <response code="200">Added product and its count</response>
        /// <response code="400">Invalid count of product</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProductToCart(Orders orders)
        {
            if (orders.Count <= 0)
            {
                return BadRequest("Count must be positive");
            }

            if (!_productRepository.Exists(orders.ProductId))
            {
                return BadRequest("Product does not exist");
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

            var newOrders = CartLogic.AddProductToCart(order, await _productRepository.GetById(orders.ProductId), orders.Count);
            await _ordersRepository.Create(newOrders);
            await _ordersRepository.Save();

            return Json(newOrders);
        }

        /// <summary>
        /// Change count of product
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>Status code</returns>
        /// <response code="204">Added product and its count</response>
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

            return NoContent();
        }

        /// <summary>
        /// Delete product from cart
        /// </summary>
        /// <param name="productInCartId"></param>
        /// <returns>Status code</returns>
        /// <response code="204">Product was deleted</response>
        /// <response code="403">Access denied</response>
        /// <response code="406">No such id</response>
        [HttpDelete("{productInCartId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(int productInCartId)
        {
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);
            var orders = await _ordersRepository.Include(o => o.OrderNavigation).FirstOrDefaultAsync(o => o.OrdersId == productInCartId);

            if (orders == null)
            {
                return StatusCode(406);
            }
            else if (orders.OrderNavigation.UserId != currentUser.UserId)
            {
                return StatusCode(403);
            }
            else
            {
                await _ordersRepository.Delete(orders.OrdersId);
                await _ordersRepository.Save();
            }

            return NoContent();
        }
    }
}
