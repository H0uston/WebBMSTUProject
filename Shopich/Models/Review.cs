using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace Shopich.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ReviewText { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        [Required]
        public int ReviewRating { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
