namespace Entities.Enums;

/// <summary>
/// Order Receipt cases
/// </summary>
public enum enOrderReceiptStatus
{
    FullyReceipt = 1,
    PartiallyReceipt,
    RefusedReceipt,

    // ReceiptRefusedDeliveryCostPaid,
    // ReceiptRefusedDeliveryCostNotPaid,
    // ReceiptRefusedDeliveryCostPartiallyPaid,
    AttemptFailed,
}
