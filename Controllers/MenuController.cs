
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dto;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MenuController : ControllerBase
{
    public resturentContext _db;

    public MenuController(resturentContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var res = _db.RestaurantMenus.ToList();

        return Ok(res);
    }

    [HttpPost]
    public IActionResult Store([FromBody] RestaurantMenusDto dto)
    {

        var resturant = _db.Restaurants.Find(dto.RestaurantId);
        var res = _db.RestaurantMenus.Add(new RestaurantMenu
        {
         MealName   = dto.MealName,
         PriceInNis = dto.PriceInNis,
         PriceInUsd = (dto.PriceInNis / 3.50f),
         Archived = dto.Archived,
         CreatedDate = DateTime.Now,
         UpdatedDate = DateTime.Now,
         Quantity = dto.Quantity,
         Restaurant = resturant
            
        }).Entity;
        _db.SaveChanges();

        return Ok(res);

    }
    
    [HttpPut]
    public IActionResult Update(int Id , [FromBody] RestaurantMenusDto dto)
    {

        var resturant = _db.Restaurants.Find(dto.RestaurantId);
        var select = _db.RestaurantMenus.Find(Id);
        select.MealName = dto.MealName;
        select.PriceInNis = dto.PriceInNis;
        select.PriceInUsd = (dto.PriceInNis / 3.50f);
        select.Quantity = dto.Quantity;
        select.Archived = dto.Archived;
        select.Restaurant = resturant;
        select.UpdatedDate = DateTime.Now;
        _db.SaveChanges();
        return Ok(select);

    }
    
    [HttpGet]
    public IActionResult GetId(int Id)
    {
        var res = _db.RestaurantMenus.Find(Id);
        return Ok(res);

    }
    
    [HttpDelete]
    public IActionResult Delete(int Id)
    {

        var select = _db.RestaurantMenus.Find(Id);
        var res = _db.RestaurantMenus.Remove(select);
        _db.SaveChanges();
        return Ok(select);

    }
}