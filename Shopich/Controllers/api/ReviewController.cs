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
    [Route("api/v2/review")]
    public class ReviewController : Controller
    {
        private readonly ShopichContext _context;

        public ReviewController(ShopichContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Review>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var reviews = await _context.Reviews.ToArrayAsync();
            return reviews.Skip((current - 1) * size).Take(size);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Review> Create(Review review)
        {
            // TODO
            var user = await _context.Users.FirstAsync(u => u.UserEmail == User.Identity.Name);
            review.UserId = user.UserId;
            review.ReviewDate = DateTime.UtcNow;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Review> Change(Review newReview)
        {
            var oldReview = await _context.Reviews.FirstAsync(r => r.ReviewId == newReview.ReviewId);
            oldReview.ReviewDate = DateTime.UtcNow;
            oldReview.ReviewText = newReview.ReviewText;
            oldReview.ReviewRating = newReview.ReviewRating;
            _context.Reviews.Update(oldReview);
            await _context.SaveChangesAsync();

            return oldReview;
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var review = await _context.Reviews.FirstAsync(r => r.ReviewId == reviewId);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}