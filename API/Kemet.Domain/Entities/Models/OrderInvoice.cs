using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class OrderInvoice
{
    [Key]
    public int OrderInvoiceId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    public virtual Order Order { get; set; }

    [ForeignKey("Return")]
    public int ReturnId { get; set; }

    public virtual Return Return { get; set; }

    public decimal TotalAmount { get; set; }

    
}
