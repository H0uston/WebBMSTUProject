using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace Shopich.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryCollection = new HashSet<Categories>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public virtual ICollection<Categories> CategoryCollection { get; set; }
    }
}
