using Entities.Enums;

namespace Entities
{
    public static class Order_RECEIPT_STATUS_Mapper
    {
        /// <summary>
        /// Mapping Order Status to Order Receipt Status
        /// </summary> <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static Dictionary<
            enOrderStatus,
            List<enOrderReceiptStatus?>?
        > OrderStatusToReceiptMap { get; } = CreateOrderStatusToReceiptMap();

        private static Dictionary<
            enOrderStatus,
            List<enOrderReceiptStatus?>?
        > CreateOrderStatusToReceiptMap()
        {
            Dictionary<enOrderStatus, List<enOrderReceiptStatus?>?> dic = new();
            dic.Add(enOrderStatus.Pending, null);
            dic.Add(enOrderStatus.Processing, null);
            dic.Add(enOrderStatus.Shipped, null);

            dic.Add(
                enOrderStatus.Delivered,
                new()
                {
                    enOrderReceiptStatus.FullyReceipt,
                    enOrderReceiptStatus.PartiallyReceipt,
                    enOrderReceiptStatus.RefusedReceipt,
                }
            );

            dic.Add(
                enOrderStatus.CancelledByCustomer,
                new() { null, enOrderReceiptStatus.RefusedReceipt }
            );

            dic.Add(enOrderStatus.CancelledByAdmin, null);

            dic.Add(
                enOrderStatus.Refunded,
                new()
                {
                    enOrderReceiptStatus.FullyReceipt,
                    enOrderReceiptStatus.PartiallyReceipt,
                    enOrderReceiptStatus.RefusedReceipt,
                    enOrderReceiptStatus.DeliveryAttemptFailed,
                }
            );

            return dic;
        }

        /// <summary>
        /// Mapping Receipt Status to Order Status
        /// </summary> <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public static Dictionary<
            enOrderReceiptStatus,
            List<enOrderStatus?>?
        > OrderReceiptStatusToOrderStatusMap { get; } = CreateOrderReceiptStatusToOrderStatusMap();

        private static Dictionary<
            enOrderReceiptStatus,
            List<enOrderStatus?>?
        > CreateOrderReceiptStatusToOrderStatusMap()
        {
            Dictionary<enOrderReceiptStatus, List<enOrderStatus?>?> dic = new();

            dic.Add(
                enOrderReceiptStatus.FullyReceipt,
                new() { enOrderStatus.Delivered, enOrderStatus.Refunded }
            );
            dic.Add(
                enOrderReceiptStatus.PartiallyReceipt,
                new() { enOrderStatus.Delivered, enOrderStatus.Refunded }
            );

            dic.Add(
                enOrderReceiptStatus.RefusedReceipt,
                new()
                {
                    enOrderStatus.Delivered,
                    enOrderStatus.Shipped,
                    enOrderStatus.CancelledByCustomer,
                }
            );
            dic.Add(
                enOrderReceiptStatus.DeliveryAttemptFailed,
                new()
                {
                    enOrderStatus.Shipped,
                    enOrderStatus.CancelledByCustomer,
                    enOrderStatus.Refunded,
                }
            );

            return dic;
        }
    }
}
