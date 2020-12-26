using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace Shopich.Models
{
    public partial class Categories
    {
        public int CategoriesId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
