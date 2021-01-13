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
    public class OrdersController : Controller
    {
        private readonly IOrders _ordersRepository;
        private readonly IOrder _orderRepository;
        private readonly IProduct _productRepository;

        public OrdersController(IOrders ordersRepository, IOrder orderRepository, IProduct productRepository)
        {
            _ordersRepository = ordersRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var shopichContext = _ordersRepository.Include(o => o.OrderNavigation).Include(o => o.Product);
            return View(await shopichContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _ordersRepository
                .Include(o => o.OrderNavigation)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await _orderRepository.GetAll(), "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdersId,OrderId,ProductId,Count")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                await _ordersRepository.Create(orders);
                await _ordersRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(await _orderRepository.GetAll(), "OrderId", "OrderId", orders.OrderId);
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", orders.ProductId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _ordersRepository.GetById((int)id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(await _orderRepository.GetAll(), "OrderId", "OrderId", orders.OrderId);
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", orders.ProductId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdersId,OrderId,ProductId,Count")] Orders orders)
        {
            if (id != orders.OrdersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ordersRepository.Update(orders);
                    await _ordersRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrdersId))
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
            ViewData["OrderId"] = new SelectList(await _orderRepository.GetAll(), "OrderId", "OrderId", orders.OrderId);
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", orders.ProductId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _ordersRepository
                .Include(o => o.OrderNavigation)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrdersId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ordersRepository.Delete(id);
            await _ordersRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _ordersRepository.Exists(id);
        }
    }
}
