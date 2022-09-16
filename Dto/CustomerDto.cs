namespace WebApplication3.Dto;

public class CustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public sbyte? Archived { get; set; }
}