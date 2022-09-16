using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dto;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderController : ControllerBase
{
    public resturentContext _db;

    public OrderController(resturentContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var res = _db.Orders.ToList();

        return Ok(res);
    }

    [HttpPost]
    public IActionResult Store([FromBody] OrderDto dto)
    {

        var cust = _db.Customers.Find(dto.CustomerId);
        var resMenu = _db.RestaurantMenus.Find(dto.RestaurantMenuId);
        var res = _db.Orders.Add(new Order
        {
            CustomerId = cust.Id,
            RestaurantMenuId = resMenu.Id
            
        }).Entity;
        _db.SaveChanges();

        return Ok(res);

    }
    
    [HttpPut]
    public IActionResult Update(int Id , [FromBody] OrderDto dto)
    {

        var select = _db.Orders.Find(Id);
        select.RestaurantMenuId = dto.RestaurantMenuId;
        select.CustomerId = dto.CustomerId;
        _db.SaveChanges();
        return Ok(select);

    }
    
    [HttpGet]
    public IActionResult GetId(int Id)
    {
        var res = _db.Orders.Find(Id);
        return Ok(res);

    }
    
    [HttpDelete]
    public IActionResult Delete(int Id)
    {

        var select = _db.Orders.Find(Id);
        var res = _db.Orders.Remove(select);
        _db.SaveChanges();
        return Ok(select);

    }
}