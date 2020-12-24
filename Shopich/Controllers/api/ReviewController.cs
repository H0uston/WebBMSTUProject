﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IEnumerable<Review>> GetAll([FromQuery] int current = 1, [FromQuery] int size = 5)
        {
            var reviews = await _reviewRepository.GetAll();
            return reviews.Skip((current - 1) * size).Take(size);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Review> Create(Review review)
        {
            // TODO
            var user = await _userRepository.GetByEmail(User.Identity.Name);
            review.UserId = user.UserId;
            review.ReviewDate = DateTime.UtcNow;
            _reviewRepository.Create(review);
            _reviewRepository.Save();

            return review;
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Review> Change(Review newReview)
        {
            var oldReview = await _reviewRepository.GetById(newReview.ReviewId);
            oldReview.ReviewDate = DateTime.UtcNow;
            oldReview.ReviewText = newReview.ReviewText;
            oldReview.ReviewRating = newReview.ReviewRating;
            _reviewRepository.Update(oldReview);
            _reviewRepository.Save();

            return oldReview;
        }

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