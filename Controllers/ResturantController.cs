using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Dto;
namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ResturantController : ControllerBase
{

    public resturentContext _db;

    public ResturantController(resturentContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var res = _db.Restaurants.ToList();

        return Ok(res);
    }

    [HttpPost]
    public IActionResult Store([FromBody] ResturantDto dto)
    {
        var res = _db.Restaurants.Add(new Restaurant
        {
            Name = dto.Name,
            Archived = dto.Archived,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            PhoneNumber = dto.PhoneNumber
            
        }).Entity;
        _db.SaveChanges();

        return Ok(res);

    }
    
    [HttpPut]
    public IActionResult Update(int Id , [FromBody] ResturantDto dto)
    {

        var select = _db.Restaurants.Find(Id);
        select.Name = dto.Name;
        select.PhoneNumber = dto.PhoneNumber;
        select.Archived = dto.Archived;
        select.UpdatedDate = DateTime.Now;
        _db.SaveChanges();

        return Ok(select);

    }
    
    [HttpGet]
    public IActionResult GetId(int Id)
    {
        var res = _db.Restaurants.Find(Id);
        return Ok(res);

    }
    
    [HttpDelete]
    public IActionResult Delete(int Id)
    {

        var select = _db.Restaurants.Find(Id);
        var res = _db.Restaurants.Remove(select);
        _db.SaveChanges();
        return Ok(select);

    }
    
}