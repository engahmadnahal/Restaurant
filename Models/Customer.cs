using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public sbyte? Archived { get; set; }
    }
}
