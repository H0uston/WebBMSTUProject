using Shopich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopich.Business_logic
{
    public class ReviewLogic
    {
        static public Review CreateReview(string reviewText, int reviewRating, int productId, User user)
        {

            if (reviewRating < 0 || reviewRating > 10)
            {
                return null;
            }

            var formedReview = new Review();

            formedReview.UserId = user.UserId;
            formedReview.ProductId = productId;
            formedReview.ReviewDate = DateTime.UtcNow;
            formedReview.ReviewRating = reviewRating;
            formedReview.ReviewText = reviewText;

            return formedReview;
        }

        static public Review UpdateReview(Review review, string reviewText, int reviewRating)
        {
            if (reviewRating < 0 || reviewRating > 10)
            {
                return null;
            }

            review.ReviewDate = DateTime.UtcNow;
            review.ReviewText = reviewText;
            review.ReviewRating = reviewRating;

            return review;
        }
    }
}
