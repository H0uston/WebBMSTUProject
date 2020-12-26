using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace Shopich.Models
{
    public partial class Orders
    {
        [Required]
        public int OrdersId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Order OrderNavigation { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
