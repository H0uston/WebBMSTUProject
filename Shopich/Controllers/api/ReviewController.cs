﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        [HttpGet]
        public async Task<IEnumerable<Review>> GetAll([FromRoute] int productId, [FromQuery] int current = 1, [FromQuery] int size = 5)
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
        public async Task<IActionResult> Create(Review review)
        {
            var user = await _userRepository.GetByEmail(User.Identity.Name);

            var formedReview = ReviewLogic.CreateReview(review, user);
            _reviewRepository.Create(review);
            _reviewRepository.Save();

            return CreatedAtAction("review", formedReview);
        }

        /// <summary>
        /// Change review for product
        /// </summary>
        /// <param name="review"></param>
        /// <returns>Changed review</returns>
        /// <response code="200"></response>
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Review> Change(Review review)
        {
            var oldReview = await _reviewRepository.GetById(review.ReviewId);

            ReviewLogic.UpdateReview(oldReview, review.ReviewText, review.ReviewRating);

            _reviewRepository.Update(oldReview);
            _reviewRepository.Save();

            return oldReview;
        }

        /// <summary>
        /// Delete review
        /// </summary>
        /// <param name="reviewId">id of review</param>
        /// <returns>Delete review</returns>
        /// <response code="204"></response>
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int reviewId)
        {
            _reviewRepository.Delete(reviewId);
            _reviewRepository.Save();

            return NoContent();
        }
    }
}