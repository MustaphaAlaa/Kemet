namespace Entities.Enums;

/// <summary>
/// Represents the payment status of an order and its delivery cost.
/// </summary>
public enum enPaymentStatus : byte
{
    /// <summary>
    /// The full order amount, including delivery cost, has been collected.
    /// </summary>
    FullyPaid = 1,

    /// <summary>
    /// A partial amount of the total order and delivery cost has been collected.
    /// </summary>
    PartiallyPaid,

    /// <summary>
    /// No payment has been collected for either the order or delivery.
    /// </summary>
    NotCollected,

    /// <summary>
    /// The customer has been refunded.
    /// </summary>
    Refunded,
}
