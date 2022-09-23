using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        

        var resMenu = _db.RestaurantMenus.Find(dto.RestaurantMenuId);
        
        if (isAvailable(resMenu))
        {
            var cust = _db.Customers.Find(dto.CustomerId);
            var res = _db.Orders.Add(new Order()
            {
                Customer = cust,
                RestaurantMenu = resMenu
            }).Entity;
            
            _db.SaveChanges();

            return Ok(res);
        }
        
        return BadRequest("Quantity is not available");

    }
    
    [HttpPut]
    public IActionResult Update(int Id , [FromBody] OrderDto dto)
    {

        var resMenu = _db.RestaurantMenus.Find(dto.RestaurantMenuId);
        
        if (isAvailable(resMenu))
        {
            var cust = _db.Customers.Find(dto.CustomerId);
            var select = _db.Orders.Find(Id);
            select.RestaurantMenuId = resMenu.Id;
            select.CustomerId = cust.Id;
            select.Customer = cust;
            select.RestaurantMenu = resMenu;
            _db.SaveChanges();
            return Ok(select);
        }
        return BadRequest("Quantity is not available");
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

    public static bool isAvailable(RestaurantMenu res)
    {
        return res.Quantity > 0;
    }


    [HttpGet]
    public IActionResult Csv()
    {
        var data = _db.RestViewTables.ToList();

        using (var writer = new StreamWriter("\\Users\\ahmad\\Desktop\\data.csv"))
        using (var csv = new CsvWriter(writer,CultureInfo.InvariantCulture))
        {
            foreach (var d in data)
            {
                csv.WriteRecord(d);
            }
        }
       
        return Ok("Success");
    }
}