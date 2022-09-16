using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public partial class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public sbyte? Archived { get; set; }
    }
}
