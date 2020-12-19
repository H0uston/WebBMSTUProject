using System;
using System.Collections.Generic;

#nullable disable

namespace Shopich.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewRating { get; set; }

        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
