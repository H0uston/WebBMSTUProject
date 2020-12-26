using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Business_logic
{
    public class ReviewLogic
    {
        static public Review CreateReview(Review review, User user)
        {
            var formedReview = new Review();

            formedReview.UserId = user.UserId;
            formedReview.ProductId = review.ProductId;
            formedReview.ReviewDate = DateTime.UtcNow;
            formedReview.ReviewRating = review.ReviewRating;
            formedReview.ReviewText = review.ReviewText;

            return formedReview;
        }

        static public Review UpdateReview(Review review, string reviewText, int reviewRating)
        {
            review.ReviewDate = DateTime.UtcNow;
            review.ReviewText = reviewText;
            review.ReviewRating = reviewRating;

            return review;
        }
    }
}
