namespace WebApplication3.Dto;

public class RestaurantMenusDto
{

    public int Id { get; set; }
    public string MealName { get; set; }
    public float? PriceInNis { get; set; }
    public int? Quantity { get; set; }
    public sbyte? Archived { get; set; }
    public int RestaurantId { get; set; }

}