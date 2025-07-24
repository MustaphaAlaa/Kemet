using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class DeliveryCostPayment
{
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
    

    [ForeignKey("Governorate")]
    public int GovernorateId { get; set; }
    public Governorate Governorate { get; set; }

    [ForeignKey("DeliveryCompany")]
    public int DeliveryCompanyId { get; set; }
    public DeliveryCompany DeliveryCompany { get; set; }

    [ForeignKey("GovernorateDeliveryCompany")]
    public int? GovernorateDeliveryCompanyId { get; set; }
    public GovernorateDeliveryCompany GovernorateDeliveryCompany { get; set; } // contains actual delivery cost

    [ForeignKey("DeliveryPaymentStatus")]
    public int? DeliveryPaymentStatusId { get; set; }
    public DeliveryPaymentStatus DeliveryPaymentStatus { get; set; } // contains actual delivery cost

    public decimal PaidByCustomer { get; set; } // From Payment
    public decimal PaidToCompany { get; set; } // From DeliveryCompanyTransaction

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
