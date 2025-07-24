namespace Entities.Enums;

public enum enDeliveryPaymentStatus
{
    /// <summary>
    /// Delivery cost has been fully collected.
    /// </summary>
    DeliveryFullPaid = 1,

    /// <summary>
    /// Only part of the delivery cost has been collected.
    /// </summary>
    DeliveryPartiallyPaid,

    /// <summary>
    /// Delivery cost is pending; payment has not been fully collected.
    /// </summary>
    DeliveryPending,

    /// <summary>
    /// Delivery cost has not been collected at all.
    /// </summary>
    DeliveryUnpaid,
}
