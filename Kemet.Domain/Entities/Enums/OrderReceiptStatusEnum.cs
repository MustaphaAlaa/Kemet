namespace Entities.Enums;

/// <summary>
/// Order Receipt cases
/// </summary>
public enum OrderReceiptStatus
{
    FullyReceipt = 1,
    PartiallyReceipt,
    RefusedReceipt,

    // ReceiptRefusedDeliveryCostPaid,
    // ReceiptRefusedDeliveryCostNotPaid,
    // ReceiptRefusedDeliveryCostPartiallyPaid,
    AttemptFailed,
}
