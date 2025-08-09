# Order Status Management

## UpdateOrderStatus Method

Description

Updates the status of an order and its associated receipt status based on business rules and validations.
Signature

```C#
public async Task<OrderStatus_OrderReceipt> UpdateOrderStatus(OrderStatus_OrderReceipt orderStatus_OrderReceipt)
```

Parameters

    orderStatus_OrderReceipt: An object containing:

        OrderId: The ID of the order to update

        OrderStatusId: The new status ID for the order

        OrderReceiptStatusId: (Optional) The new receipt status ID

Returns

An OrderStatus_OrderReceipt object with the updated status values.
Business Rules and Validations

    Validates the order status ID exists

    Verifies the order exists

    For certain statuses (Delivered, Shipped, Refunded), requires a delivery company to be assigned

    Automatically sets appropriate receipt status based on order status if not provided

    Updates the UpdatedAt timestamp

Status Mapping Logic

    Uses Order_RECEIPT_STATUS_Mapper.OrderStatusToReceiptMap to determine valid receipt statuses

    If no receipt status is provided, uses the first valid receipt status for the order status

    Some order statuses don't require receipt statuses (set to null)

Error Cases

    Throws exception if:

        Order doesn't exist

        Order status ID is invalid

        Trying to set Delivered/Shipped/Refunded status without delivery company

        Invalid receipt status for the order status

## UpdateOrderReceiptStatus Method

Description

Updates the receipt status of an order and may update the order status based on business rules.

```C#
public async Task<OrderStatus_OrderReceipt> UpdateOrderReceiptStatus(OrderStatus_OrderReceipt orderStatus_OrderReceipt)
```

Parameters

    orderStatus_OrderReceipt: An object containing:

        OrderId: The ID of the order to update

        OrderReceiptStatusId: The new receipt status ID

        OrderStatusId: The current order status ID (required)

Returns

An OrderStatus_OrderReceipt object with the updated status values.
Business Rules and Validations

    Validates the order status ID exists

    Verifies the order exists

    Requires a valid order status ID

    Updates the order status based on receipt status if mapping exists

    Updates the UpdatedAt timestamp

Status Mapping Logic

    Uses Order_RECEIPT_STATUS_Mapper.OrderReceiptStatusToOrderStatusMap to determine valid order statuses

    Ensures the provided receipt status is compatible with the current order status

Error Cases

    Throws exception if:

        Order doesn't exist

        Order status ID is null or invalid

        Invalid receipt status for the order status

Order_RECEIPT_STATUS_Mapper Class
Description

Static class that maintains the mappings between order statuses and receipt statuses.
Mappings
Order Status to Receipt Status Map

Defines what receipt statuses are valid for each order status:
Order Status Valid Receipt Statuses

|       Status        |                                 Value                                 |
| :-----------------: | :-------------------------------------------------------------------: |
|       Pending       |                                 None                                  |
|     Processing      |                                 None                                  |
|       Shipped       |                                 None                                  |
|      Delivered      |            FullyReceipt, PartiallyReceipt, RefusedReceipt             |
| CancelledByCustomer |                         None, RefusedReceipt                          |
|  CancelledByAdmin   |                                 None                                  |
|      Refunded       | FullyReceipt, PartiallyReceipt, RefusedReceipt, DeliveryAttemptFailed |

Receipt Status to Order Status Map

Defines what order statuses are valid for each receipt status:
Receipt Status Valid Order Statuses

|    Receipt Status     |             Order Statuses              |
| :-------------------: | :-------------------------------------: |
|     FullyReceipt      |           Delivered, Refunded           |
|   PartiallyReceipt    |           Delivered, Refunded           |
|    RefusedReceipt     | Delivered, Shipped, CancelledByCustomer |
| DeliveryAttemptFailed | Shipped, CancelledByCustomer, Refunded  |

Usage

The mappings are used by the update methods to validate status transitions and automatically set related statuses when appropriate.

Status Enumeration Values
enOrderStatus

Represents the possible states of an order:

    Pending

    Processing

    Shipped

    Delivered

    CancelledByCustomer

    CancelledByAdmin

    Refunded

enOrderReceiptStatus

Represents the possible receipt statuses:

    FullyReceipt

    PartiallyReceipt

    RefusedReceipt

    DeliveryAttemptFailed

**Common Scenarios**

Scenario 1: Order Delivery Completion

    Order status changes to "Delivered"

    System automatically sets receipt status to "FullyReceipt" (default)

    Merchant can optionally change to "PartiallyReceipt" or "RefusedReceipt"

Scenario 2: Failed Delivery Attempt

    Order status is "Shipped"

    Receipt status changed to "DeliveryAttemptFailed"

    System may automatically update order status to "Refunded" or "CancelledByCustomer"

Scenario 3: Order Cancellation

    Customer cancels order (status "CancelledByCustomer")

    If already shipped, receipt status can be set to "RefusedReceipt"

    If not shipped, no receipt status needed

Scenario 4: Refund Processing

    Order status changes to "Refunded"

    System requires a receipt status to be set

    Valid options depend on refund circumstances (full, partial, refused, etc.)

**Error Handling**

Both methods follow similar error handling patterns:

    Input validation

    Business rule validation

    Status mapping validation

    Logging errors before rethrowing

Errors are logged with descriptive messages including:

    The operation that failed

    The order ID involved

    The specific error message
