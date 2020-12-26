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
    [Route("api/v1/review")]
    public class ReviewController : Controller
    {
        private readonly IReview _reviewRepository;
        private readonly IUser _userRepository;

        public ReviewController(IReview reviewRepository, IUser userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get reviews for product page
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="current">Current page</param>
        /// <param name="size">Size of page</param>
        /// <returns>List of reviews</returns>
        /// <response code="200"></response>
        [HttpGet("{productId}")]
        public async Task<IEnumerable<Review>> GetAll([FromRoute]int productId, [FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var reviews = await _reviewRepository.GetAllByProductId(productId);

            return reviews.Skip((current - 1) * size).Take(size);
        }

        /// <summary>
        /// Create review for product
        /// </summary>
        /// <param name="review"></param>
        /// <returns>Created review</returns>
        /// <response code="201"></response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateReview(Review review)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);

            if (_reviewRepository.ExistsByUserId(user.UserId))
            {
                return BadRequest("Review already exist");
            }

            var formedReview = ReviewLogic.CreateReview(review, user);
            await _reviewRepository.Create(review);
            await _reviewRepository.Save();

            return CreatedAtAction("review", formedReview);
        }

        /// <summary>
        /// Change review for product
        /// </summary>
        /// <param name="review"></param>
        /// <returns>Changed review</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeReview(Review review)
        {
            var oldReview = await _reviewRepository.GetById(review.ReviewId);
            var user = await _userRepository.GetByEmail(User.Identity.Name);

            if (user.UserId != review.UserId)
            {
                return BadRequest("Can't change another user's review");
            }    

            ReviewLogic.UpdateReview(oldReview, review.ReviewText, review.ReviewRating);

            _reviewRepository.Update(oldReview);
            await _reviewRepository.Save();

            return Json(oldReview);
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <param name="reviewId">id of review</param>
        /// <returns>Delete review</returns>
        /// <response code="204"></response>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            var review = await _reviewRepository.GetById(reviewId);

            if (user.UserId != review.UserId)
            {
                return BadRequest("Can't change another user's review");
            }

            _reviewRepository.Delete(reviewId);
            await _reviewRepository.Save();

            return NoContent();
        }
    }
}