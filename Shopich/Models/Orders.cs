using System;
using System.Collections.Generic;

#nullable disable

namespace Shopich.Models
{
    public partial class Orders
    {
        public int OrdersId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        public virtual Order OrderNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
