namespace Entities.Enums;

/// <summary>
/// Order Receipt cases
/// </summary>
public enum OrderReceiptStatusEnum
{
    FullyReceipt = 1,
    PartiallyReceipt,
    ReceiptRefusedDeliveryCostPaid,
    ReceiptRefusedDeliveryCostNotPaid,
    ReceiptRefusedDeliveryCostPartiallyPaid,
    AttemptFailed
}
