using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shopich.Models;
using Microsoft.AspNetCore.Authorization;
using Shopich.Repositories.interfaces;

namespace Shopich.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategories _categoriesRepository;
        private readonly ICategory _categoryRepository;
        private readonly IProduct _productRepository;

        public CategoriesController(ICategories categoriesRepository, ICategory categoryRepository, IProduct productRepository)
        {
            _categoriesRepository = categoriesRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var shopichContext = _categoriesRepository.Include(c => c.Category).Include(c => c.Product);
            return View(await shopichContext.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesRepository
                .Include(c => c.Category)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CategoriesId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "CategoryName");
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriesId,ProductId,CategoryId")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                await _categoriesRepository.Create(categories);
                await _categoriesRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "CategoryName", categories.CategoryId);
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", categories.ProductId);
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesRepository.GetById((int)id);
            if (categories == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "CategoryName", categories.CategoryId);
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", categories.ProductId);
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriesId,ProductId,CategoryId")] Categories categories)
        {
            if (id != categories.CategoriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoriesRepository.Update(categories);
                    await _categoriesRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriesExists(categories.CategoriesId))
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
            ViewData["CategoryId"] = new SelectList(await _categoryRepository.GetAll(), "CategoryId", "CategoryName", categories.CategoryId);
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", categories.ProductId);
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesRepository
                .Include(c => c.Category)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(m => m.CategoriesId == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoriesRepository.Delete(id);
            await _categoriesRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriesExists(int id)
        {
            return _categoriesRepository.Exists(id);
        }
    }
}
