using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class DeliveryTransaction
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    [ForeignKey("DeliveryCompany")]
    public int DeliveryCompanyId { get; set; }
    public virtual DeliveryCompany DeliveryCompany { get; set; }

    public decimal PaidByCustomer { get; set; }
    public decimal ActualCostToCompany { get; set; }

    public decimal Difference => PaidByCustomer - ActualCostToCompany;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
