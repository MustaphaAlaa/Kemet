namespace Entities.Models;

public class Governorate
{
    public int GovernorateId { get; set; }
    public string NameAr { get; set; }
    public string? NameEn { get; set; }
    public bool IsAvailableToDeliver { get; set; }
}
