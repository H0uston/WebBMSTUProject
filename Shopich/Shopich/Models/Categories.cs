﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Shopich.Models
{
    public partial class Categories
    {
        public int CategoriesId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
