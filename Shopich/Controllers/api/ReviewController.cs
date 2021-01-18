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
    [Route("api/v1/products/{productId}/reviews")]
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
        /// <returns>List of reviews</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<IEnumerable<Review>> GetAll([FromRoute]int productId)
        {
            var reviews = await _reviewRepository.GetAllByProductId(productId);

            for (var i = 0; i < reviews.Length; i++)
            {
                var user = await _userRepository.GetById(reviews[i].UserId);

                reviews[i].User = new User { UserId = user.UserId, UserName = user.UserName, UserSurname = user.UserSurname, UserEmail = user.UserEmail };
            }

            return reviews;
        }

        /// <summary>
        /// Create review for product
        /// </summary>
        /// <param name="review"></param>
        /// <param name="productId"></param>
        /// <returns>Created review</returns>
        /// <response code="201">Created At Action</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview([FromRoute] int productId, Review review)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);

            if (_reviewRepository.ExistsByUserId(user.UserId))
            {
                return BadRequest("Review already exist");
            }   

            var formedReview = ReviewLogic.CreateReview(review.ReviewText, review.ReviewRating, productId, user);

            if (formedReview == null)
            {
                return BadRequest("ReviewRating must by 0 <= x <= 10");
            }
            await _reviewRepository.Create(formedReview);
            await _reviewRepository.Save();

            return CreatedAtAction("review", formedReview);
        }

        /// <summary>
        /// Change review for product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="review">Updated review</param>
        /// <returns>Changed review</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeReview([FromRoute] int productId, Review review)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            var oldReview = await _reviewRepository.GetByProductIdAndUserId(productId, user.UserId);

            if (oldReview == null)
            {
                return BadRequest("Review does not exist");
            }

            ReviewLogic.UpdateReview(oldReview, review.ReviewText, review.ReviewRating);

            if (oldReview == null)
            {
                return BadRequest("ReviewRating must by 1 <= x <= 5");
            }

            _reviewRepository.Update(oldReview);
            await _reviewRepository.Save();

            return Json(oldReview);
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="reviewId">id of review</param>
        /// <returns>Delete review</returns>
        /// <response code="204"></response>
        [HttpDelete("{reviewId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteReview([FromRoute] int productId, int reviewId)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            var review = await _reviewRepository.GetById(reviewId);

            if (review == null)
            {
                return BadRequest("Review does not exist");
            }
            else if (user.UserId != review.UserId)
            {
                return BadRequest("Can't change another user's review");
            }

            await _reviewRepository.Delete(reviewId);
            await _reviewRepository.Save();

            return NoContent();
        }
    }
}