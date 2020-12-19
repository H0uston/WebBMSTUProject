using System;
using System.Collections.Generic;

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
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public bool ProductAvailability { get; set; }
        public int? ProductDiscount { get; set; }

        public virtual ICollection<Categories> CategoryCollection { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
