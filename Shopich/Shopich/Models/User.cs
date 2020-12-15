using System;
using System.Collections.Generic;

#nullable disable

namespace Shopich.Models
{
    public partial class User
    {
        public User()
        {
            OrderCollection = new HashSet<Orders>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserCity { get; set; }
        public string UserStreet { get; set; }
        public string UserHouse { get; set; }
        public string UserFlat { get; set; }
        public int? UserIndex { get; set; }
        public DateTime? UserBirthday { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Orders> OrderCollection { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
