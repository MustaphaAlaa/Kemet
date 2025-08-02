# Order Management

When a customer want to order quantity more than that offers in the ProductQuantityPrice table
s/he contact direct with the sales department to make a special order.

contact with sales department will be via whatsapp api

- ## Receipt Order Status

  - ### Theses cases for the order receipt status:

    - Customer received the order and paid for it. (Fully Receipt).
    - Customer received a part from the order and paid for that part. (Partially Receipt).
    - Customer refused to receive the order, and paid for the delivery cost. (ReceiptRefusedDeliveryCostPaid).
    - Customer refused to receive the order, and paid part of the delivery cost. (ReceiptRefusedDeliveryCostPartiallyPaid).
    - Customer refused to receive the order, and didn't pay the delivery cost. (ReceiptRefusedDeliveryCostNotPaid).
    - Failed to reach to the customer. (AttemptFailed).

- ## Order DTO Notes

  There is no OrderStatusID and OrderReceiptStatusId In OrderCreateDTO Because
  in while creating the order, the order status is always pending and the order receipt status is always null.

- ## Order Receipt Status

  Order Receipt Status cannot accendently back to null
  so should updating it be in a seperate method, api controller.
  but Update Methid in OrderService, while validation the update request order Receipt cannot back to null

- ## Order-Delivery

  the order should has a foreign key for the delivery company.

- ## Delivery Company To The Order
  - When assign delivery company to the order, should validate if order's status if it's not pending or in process it'cant be assign because it'll be already received to the delivery company.
  - Retrieve the latest governorate delivery company and assign it to the order;
