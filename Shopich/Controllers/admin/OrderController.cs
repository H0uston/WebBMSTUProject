using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using Shopich.Repositories.interfaces;

namespace Shopich.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IOrder _orderRepository;
        private readonly IUser _userRepository;

        public OrderController(IOrder orderRepository, IUser userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = _orderRepository.Include(o => o.User);
            return View(await orders.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,OrderDate,IsApproved")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.Create(order);
                await _orderRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail", order.UserId);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository.GetById((int)id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail", order.UserId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,OrderDate,IsApproved")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderRepository.Update(order);
                    await _orderRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail", order.UserId);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderRepository
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _orderRepository.Delete(id);
            await _orderRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _orderRepository.Exists(id);
        }
    }
}
