namespace Entities.Models.DTOs;

public class GovernorateReadDTO
{
    public int GovernorateId { get; set; }
    public string NameAr { get; set; }
    public string? NameEn { get; set; }
    public bool IsAvailableToDeliver { get; set; }

}
