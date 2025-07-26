using Entities.Enums;

namespace Entities.Models.DTOs;

public class OrderCreateDTO
{
    public Guid CustomerId { get; set; }
    public int AddressId { get; set; }
    public int ProductId { get; set; }
    public decimal OrderTotalPrice { get; set; }

    public int OrderStatusId { get; set; } = (int)enOrderStatus.Pending;
    public int ProductQuantityPriceId { get; set; }
}
