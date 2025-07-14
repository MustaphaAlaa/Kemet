namespace Entities.Enums;

/// <summary>
/// enum contains Payment types
/// </summary>
public enum PaymentStatusEnum : byte
{
    OrderTotalPriceAndDeliveryCostCollected = 1,
    OrderTotalPriceAndDeliveryCostCollectedPartially,
    DeliveryCostPartiallyPaymentCollected,
    DeliveryCostPartiallyCollected,
    DeliveryCostPartiallyPayment,
    DeliveryCostNotCollected,
    Refund_Given,
}

/// <summary>
/// Represents the payment status of an order and its delivery cost.
/// </summary>
public enum PaymentStatus : byte
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

    /// <summary>
    /// No payment has been collected for either the order or delivery.
    /// </summary>
    NotCollected,

    /// <summary>
    /// The customer has been refunded.
    /// </summary>
    Refunded,
}

public enum OrderReceiptStatusEnum2
{
    FullyReceipt = 1,
    PartiallyReceipt,
    ReceiptRefusedDeliveryCostPaid,
    ReceiptRefusedDeliveryCostNotPaid,
    ReceiptRefusedDeliveryCostPartiallyPaid,
    AttemptFailed,
}
