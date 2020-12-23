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
    [Route("api/v2/order")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        private readonly ShopichContext _context;

        public OrderController(ShopichContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var orders = await _context.OrderCollection.ToArrayAsync();
            return orders.Skip((current - 1) * size).Take(size);
        }

        [Route("{id:int}")]
        public async Task<Order> GetOrder(int id)
        {
            var order = await _context.OrderCollection.FindAsync(id);
            return order;
        }

        [HttpPost]
        public async Task<Order> CreateOrder(int id)
        {
            var order = await _context.OrderCollection.FirstAsync(o => o.OrderId == id);

            order.IsApproved = true;
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = _context.OrderCollection.Find(id);
            if (order == null)
            {
                return BadRequest("No such order");
            }
            _context.OrderCollection.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
