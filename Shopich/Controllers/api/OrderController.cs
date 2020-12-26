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
        private readonly IOrders _ordersRepository;
        private readonly IUser _userRepository;

        public OrderController(IOrder orderRepository, IOrders ordersRepository, IUser userRepository)
        {
            _orderRepository = orderRepository;
            _ordersRepository = ordersRepository;
            _userRepository = userRepository;
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
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);
            var orders = await _orderRepository.GetAllAcceptedForUser(currentUser.UserId);

            return orders.Skip((current - 1) * size).Take(size);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order object</returns>
        /// <response code="200"></response>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderRepository.GetById(id);
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);

            if (order == null)
            {
                return BadRequest("No such order");
            }
            else if (order.UserId != currentUser.UserId)
            {
                return BadRequest("Try to get another user's order");
            }
            else
            {
                return Json(order);
            }
        }

        /// <summary>
        /// Create order
        /// </summary>
        /// <returns>Approved order</returns>
        /// <response code="201"></response>
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            var order = await _orderRepository.GetUnacceptedOrder((await _userRepository.GetByEmail(User.Identity.Name)).UserId);

            order = OrderLogic.ApproveOrder(order);

            _orderRepository.Update(order);
            await _orderRepository.Save();

            return CreatedAtAction("order", order);
        }


        /// <summary>
        /// Delete order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code</returns>
        /// <response code="204"></response>
        /// <response code="400"></response>
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetById(id);
            var currentUser = await _userRepository.GetByEmail(User.Identity.Name);

            if (order == null)
            {
                return BadRequest("No such order");
            }
            else if (order.UserId != currentUser.UserId)
            {
                return BadRequest("Try to delete another user's order");
            }
            else
            {
                await _orderRepository.Delete(id);
                await _orderRepository.Save();
            }

            return NoContent();
        }
    }
}
