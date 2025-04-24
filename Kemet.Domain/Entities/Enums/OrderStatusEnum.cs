namespace Entities.Enmus;

/// <summary>
/// Order Status for tracking status of the order.
/// </summary>
public enum OrderStatusEnum
{
    Pending = 1,
    Processing,
    Completed,
    CanceledByCustomer,
    CanceledByAdmin,
    Refunded
}