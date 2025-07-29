# 🐞 Bugs & Fixes

## 🔴 Open Bugs

- **Color Service**: A new color cannot be added if the name or hex code already exists — ✅ this is expected. But during an update, duplicate values can still be used — ❌ this should be prevented too.

- **Order Creation**: When creating an order request, `GovernorateDeliveryCost` is `null`. It should be automatically linked to the latest active delivery cost for the specified governorate.

- **Order Approval**: When an order is approved, product stock should decrease — this currently doesn't happen.

## ✅ Solved Bugs

- **Product Quantity Validation**:  
  When a customer places an order, the quantity should exactly match a value in `ProductQuantityPrice`. Previously, mismatched values passed — ✅ **Fixed**: now invalid quantities are rejected.
