namespace Entities.Enums;

/// <summary>
/// Order Status for tracking status of the order.
/// </summary>
public enum enOrderStatus
{
    Pending = 1,
    Processing,
    Shipped,
    Delivered,
    CancelledByCustomer,
    CancelledByAdmin,
    Refunded,
}
