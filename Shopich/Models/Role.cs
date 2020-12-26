using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Shopich.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
