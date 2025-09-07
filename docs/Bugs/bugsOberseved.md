# 🐞 Bugs & Fixes

## 🔴 Open Bugs

- **Color Service**: A new color cannot be added if the name or hex code already exists — ✅ this is expected. But during an update, duplicate values can still be used — ❌ this should be prevented too.

- **Order Creation**: When creating an order request, `GovernorateDeliveryCost` is `null`. It should be automatically linked to the latest active delivery cost for the specified governorate.

- **Order Approval**: When an order is approved, product stock should decrease — this currently doesn't happen.

- **Create Product**: When Order is creating sometimes ColorsWithItSizes colors is and sizing is adding to it, and sometimes there's no thing added at all.
                      (Priority) for this feature  is low, this is customize e-commerce and business owner is having low few products without plan to increase them in 2 years, this bug in frontend

## ✅ Solved Bugs

- **Product Quantity Validation**:  
  When a customer places an order, the quantity should exactly match a value in `ProductQuantityPrice`. Previously, mismatched values passed — ✅ **Fixed**: now invalid quantities are rejected.
