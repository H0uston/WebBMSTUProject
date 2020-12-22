using System;
using System.Collections.Generic;

#nullable disable

namespace Shopich.Models
{
    public partial class RegisterModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string house { get; set; }
        public string flat { get; set; }
        public int? index { get; set; }
        public DateTime? birthday { get; set; }
    }
}
