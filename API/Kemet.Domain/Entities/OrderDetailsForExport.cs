namespace Entities;
public class OrderDetailsForExport
{
    public int OrderId { get; set; }
    public string? OrderNote { get; set; }
    public string StreetAddress { get; set; }
    public string CustomerName { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public int orderItemsQuantity { get; set; }
    public decimal OrderTotalPrice { get; set; }
    
    public decimal DeliveryCostForCustomer { get; set; }
    public decimal DeliveryCostForCompany { get; set; }
    public string GovernorateName { get; set; }
    public string? CodeFromDeliveryCompany { get; set; }
    public string? OrderDetails { get; set; }
}
