using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace Shopich.Models
{
    public partial class Product
    {
        public Product()
        {
            CategoryCollection = new HashSet<Categories>();
            Orders = new HashSet<Orders>();
            Reviews = new HashSet<Review>();
        }

        public int ProductId { get; set; }
        public string ProductPhotoPath { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public bool ProductAvailability { get; set; }
        public int? ProductDiscount { get; set; }
        [NotMapped]
        public double? ProductRating { get; set; }
        [NotMapped]
        public double ProductPriceWithDiscount { get; set; }
        public virtual ICollection<Categories> CategoryCollection { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual ICollection<Orders> Orders { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }

        public void SetPriceWithDiscount()
        {
            this.ProductPriceWithDiscount = this.ProductPrice - (this.ProductPrice * (double)(this.ProductDiscount != null ? Math.Round((decimal)ProductDiscount / 100, 2) : 1));
        }
        public void SetProductRating()
        {
            double? rating = null;
            if (this.Reviews.Count != 0)
            {
                rating = 0;
                var r = this.Reviews.ToArray();

                for (var i = 0; i < this.Reviews.Count; i++)
                {
                    rating += r[i].ReviewRating;
                }

                rating = Math.Round((double)rating / this.Reviews.Count, 2);
            }

            this.ProductRating = rating;
        }
    }
}
