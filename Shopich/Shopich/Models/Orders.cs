using System;
using System.Collections.Generic;

#nullable disable

namespace Shopich.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderCollection = new HashSet<Order>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime? OrderDate { get; set; }
        public bool IsApproved { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> OrderCollection { get; set; }
    }
}
