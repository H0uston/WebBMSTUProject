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
    public class ReviewController : Controller
    {
        private readonly IReview _reviewRepository;
        private readonly IProduct _productRepository;
        private readonly IUser _userRepository;

        public ReviewController(IReview reviewRepository, IProduct productRepository, IUser userRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        // GET: Review
        public async Task<IActionResult> Index()
        {
            var shopichContext = _reviewRepository.Include(r => r.Product).Include(r => r.User);
            return View(await shopichContext.ToListAsync());
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewRepository
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName");
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewId,UserId,ProductId,ReviewText,ReviewDate,ReviewRating")] Review review)
        {
            if (ModelState.IsValid)
            {
                await _reviewRepository.Create(review);
                await _reviewRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", review.ProductId);
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail", review.UserId);
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewRepository.GetById((int)id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", review.ProductId);
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail", review.UserId);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewId,UserId,ProductId,ReviewText,ReviewDate,ReviewRating")] Review review)
        {
            if (id != review.ReviewId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _reviewRepository.Update(review);
                    await _reviewRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewId))
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
            ViewData["ProductId"] = new SelectList(await _productRepository.GetAll(), "ProductId", "ProductName", review.ProductId);
            ViewData["UserId"] = new SelectList(await _userRepository.GetAll(), "UserId", "UserEmail", review.UserId);
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _reviewRepository
                .Include(r => r.Product)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ReviewId == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reviewRepository.Delete(id);
            await _reviewRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _reviewRepository.Exists(id);
        }
    }
}
