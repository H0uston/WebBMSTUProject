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
        /// <param name="reviewText"></param>
        /// <param name="reviewRating"></param>
        /// <param name="productId"></param>
        /// <returns>Created review</returns>
        /// <response code="201">Created At Action</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview(string reviewText, int reviewRating, int productId)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);

            if (_reviewRepository.ExistsByUserId(user.UserId))
            {
                return BadRequest("Review already exist");
            }   

            var formedReview = ReviewLogic.CreateReview(reviewText, reviewRating, productId, user);

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
        /// <param name="reviewText"></param>
        /// <param name="reviewRating"></param>
        /// <returns>Changed review</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeReview(string reviewText, int reviewRating)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            var oldReview = await _reviewRepository.GetByUserId(user.UserId);

            if (oldReview == null)
            {
                return BadRequest("Review does not exist");
            }

            ReviewLogic.UpdateReview(oldReview, reviewText, reviewRating);

            if (oldReview == null)
            {
                return BadRequest("ReviewRating must by 0 <= x <= 10");
            }

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            var review = await _reviewRepository.GetById(reviewId);

            if (user.UserId != review.UserId)
            {
                return BadRequest("Can't change another user's review");
            }

            await _reviewRepository.Delete(reviewId);
            await _reviewRepository.Save();

            return NoContent();
        }
    }
}