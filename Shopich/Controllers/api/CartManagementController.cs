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
    [Route("api/v2/cart")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CardManagementController : Controller
    {
        private readonly ShopichContext _context;

        public CardManagementController(ShopichContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Orders>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var order = await _context.OrderCollection.Include(o => o.User).FirstAsync(o => o.User.UserEmail == User.Identity.Name && o.IsApproved == false);
            var productsInCart = await _context.Orders.Where(o => o.OrderId == order.OrderId).ToArrayAsync();
            return productsInCart.Skip((current - 1) * size).Take(size);
        }

        [HttpPost]
        public async Task<Order> AddProductToCart(int productId)
        {
            var order = await _context.OrderCollection.Include(o => o.User).FirstAsync(o => o.User.UserEmail == User.Identity.Name && o.IsApproved == false);

            if (order == null)
            {
                var now = DateTime.UtcNow;
                var user = await _context.Users.FirstAsync(u => u.UserName == User.Identity.Name);
                order = new Order();
                order.UserId = user.UserId;
                order.OrderDate = now;
                order.IsApproved = false;
                _context.OrderCollection.Add(order);
            }
            // TODO
            var orders = new Orders();
            order = await _context.OrderCollection.Include(o => o.User).FirstAsync(o => o.User.UserEmail == User.Identity.Name && o.IsApproved == false);
            orders.OrderId = order.OrderId;
            orders.ProductId = productId;
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeCount(Orders orders)
        {
            var order = await _context.OrderCollection.Include(o => o.User).FirstAsync(o => o.User.UserEmail == User.Identity.Name && o.IsApproved == false);

            var newOrders = await _context.Orders.FirstAsync(o => o.OrderId == order.OrderId && o.ProductId == orders.ProductId);
            newOrders.Count = orders.Count;

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int productId)
        {
            var order = await _context.OrderCollection.Include(o => o.User).FirstAsync(o => o.User.UserEmail == User.Identity.Name && o.IsApproved == false);
            var orders = await _context.Orders.FirstAsync(o => o.OrderId == order.OrderId && o.ProductId == productId);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
