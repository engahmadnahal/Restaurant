namespace WebApplication3.Models;

public class RestViewTable
{
    public string RestaurantName { get; set; }
    public int NumberOfOrderedCustomer { get; set; }
    public float ProfitInUsd { get; set; }
    public float ProfitInNis { get; set; }
    public  string TheBestSellingMeal { get; set; }
    public string MostPurchasedCustomer { get; set; }
}