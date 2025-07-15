# Restructure Orders, Returning Managements

## Add TO OrderItem

- ### Order Status

  - Pending (default).
  - Receipt.
  - Returned.

    **When the Parent Order's Status is:**

- FullyReceipt => All OrderItem's status will be Receipt
- PartiallyReceipt => The Request Should contain that status is PartiallyReceipt and all order that recept, others will be returned.
- refuse/AttemptFailed => all items are returned
