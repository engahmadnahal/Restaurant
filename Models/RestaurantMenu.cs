using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public partial class RestaurantMenu
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public float? PriceInNis { get; set; }
        public float? PriceInUsd { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public sbyte? Archived { get; set; }
    }
}
