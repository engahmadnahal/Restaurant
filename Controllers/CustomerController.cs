using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dto;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    public resturentContext _db;

    public CustomerController(resturentContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var res = _db.Customers.ToList();

        return Ok(res);
    }

    [HttpPost]
    public IActionResult Store([FromBody] CustomerDto dto)
    {
        var res = _db.Customers.Add(new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Archived = dto.Archived
            
        }).Entity;
        _db.SaveChanges();

        return Ok(res);

    }
    
    [HttpPut]
    public IActionResult Update(int Id , [FromBody] CustomerDto dto)
    {

        var select = _db.Customers.Find(Id);
        select.FirstName = dto.FirstName;
        select.LastName = dto.LastName;
        select.Archived = dto.Archived;
        select.UpdatedDate = DateTime.Now;
        _db.SaveChanges();
        return Ok(select);

    }
    
    [HttpGet]
    public IActionResult GetId(int Id)
    {
        var res = _db.Customers.Find(Id);
        return Ok(res);

    }
    
    [HttpDelete]
    public IActionResult Delete(int Id)
    {

        var select = _db.Customers.Find(Id);
        var res = _db.Customers.Remove(select);
        _db.SaveChanges();
        return Ok(select);

    }
}