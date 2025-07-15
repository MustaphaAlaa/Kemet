# Models

**`Ar`** = **Display Arabic UI**

## Models Overview

- ### Address

  - StreetAddress: string
    - (**`Ar`**) العنوان
  - IsActive: bool
    - (**`Ar`**) مفعل
  - ForeignKey("Governorate"):
    - (**`Ar`**) المحافظة
  - ForeignKey("Customer")

- ### Customer

        could be anonymous user or signed in user.

  - If user anonymous, these records should be entered.
    - FirstName : string?
    - LastName : string?
    - PhoneNumber : string?
  - If user signin
    - ForeignKey("User")

- ### User

- ### Role

        ASP.NET Identity Roles.

- ### Color

- ### Size

- ### Category

- ### Governorate

  - Name: string
    - (**`Ar`**) اسم المحافظة
  - IsAvailableToDeliver: bool
    - (**`Ar`**) هل متاح التوصيل اليها

- ### DeliveryCompany

        contains data about basic data about the delivery company (Name, when work with it).

- ### GovernorateDelivery

        responsible for the delivery cost that will be appear to customers.

- ### GovernorateDeliveryCompany

        contains the delivery company price for each governorate.

- ### Order

         here's the parent order table that all OrderItems will be linked with it.

  - ForeignKey("Customer")
  - ForeignKey("Address")

  - ForeignKey("OrderReceiptStatus").

    - null when the order didn't receipt yet.
      if order is receipt, it'll take value from OrderReceiptStatus table/Enum.

  - ForeignKey("OrderStatus")

    - for track the order status.
    - default value is 1 (Pending).

  - bool? IsPaid?: bool

    - will be false, when the customer refuse to receipt the order.
    - true when the order is paid.
    - null when the order didn't receipt yet.
    - public DateTime CreatedAt { get; set; }

  - public DateTime? UpdatedAt { get; set; }
    - UpdateAt will be null until first update.

- ### OrderItem

  - contains the productVariantId,
  - quantity of items for that productVariantId,
  - the unitPrice of the productVariant at the time ordering,
  - total price of orderItem => quantity \* unitPrice

- ### OrderReceiptNotes

         contains notes the order, it should be contains a foreign key to the order or orderItem, this doesn't decided yet.

- ### OrderReceiptStatus

  - FullyReceipt
    - (**`Ar`**) تم الاستلام بالكامل,
  - PartiallyReceipt
    - (**`Ar`**) تم استلام جزء من الطلب,
  - ReceiptRefusedDeliveryCostPaid
    - (**`Ar`**) العميل رفض الطلب ودفع تكلفة الشحن,
  - ReceiptRefusedDeliveryCostNotPaid
    - (**`Ar`**) العميل رفض الطلب ورفض دفع تكلفة الشحن,
  - ReceiptRefusedDeliveryCostPartiallyPaid
    - (**`Ar`**) العميل رفض الطلب ودفع جزء من تكلفة الشحن ,
  - AttemptFailed
    - (**`Ar`**) فشل الوصول للعميل

- ### OrderStatus

  - Pending
    - (**`Ar`**) معلق
  - Processing
    - (**`Ar`**) تتم المعالجة
  - Completed
    - (**`Ar`**) تم,
  - CanceledByCustomer
    - (**`Ar`**) تم الالغاء بواسطة العميل,
  - CanceledByAdmin
    - (**`Ar`**) تم الالغاء بواسطة المسؤول,
  - Refunded / Returned
    - (**`Ar`**) تم استرداد المبلغ / مرتجع

- ### Return

      For tracking the items that returned from the order 

  - ForeignKey("OrderItem")
  - ForeignKey("User")

    - int ReturnedBy => userId

  - Quantity: int

  - the number of returned items.

  - ReturnDate: DateTime
  - Notes?: string
  - HasIssue: bool
    - if the item has problem.
  - IsRestocked: bool
    - if the item backed to the inventory or not.

- ### Product

  - basic info about the product e.g:
    - Product Name: string
    - CategoryId: int.
    - Description: string.
    - CreatedAt: DateTime.
    - UpdateAt: DateTime.

- ### ProductVariant

        Each product has different colors, sizes, StockQuantity each record of this table will be for this variant.

- ### Price

  - MinimumPrice: decimal
    - (**`Ar`**) الحد الادنى للسعر
  - MaximumPrice:decimal
    - (**`Ar`**) الحد الاقصى
  - in offers case
    - public DateTime? StartFrom: DateTime?
    - public DateTime? EndsAt: DateTime?
  - Note?: string
  - IsActive: bool => it used also for soft delete
  - CreatedAt: DateTime.
  - ForeignKey("Product")

- ### Payment

  - ForeignKey("Order")
  - Amount: decimal
    - (**`Ar`**) المبلغ
  - PaymentDate
  - ForeignKey("PaymentType")

- ### PaymentType

  - DeliveryCostPayment
    - (**`Ar`**) تم دفع تكلفة الشحن
  - DeliveryCostPartiallyPayment
    - (**`Ar`**) تم دفع جزء من تكلفة الشحن
