namespace Entities.Models.DTOs;

public class GetCustomerOrdersInfo
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string StreetAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string GovernorateName { get; set; }
}
