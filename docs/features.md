# Features

## CRUD Features

- Colors.
- Sizes.
- Governorate.
-

## Logics && Features

- Each product has different colors and sizes (ProductVariant).
- Each product variant has a different stock.
- Management for product variant's stock.
- Management for products.
- Price management
- Inventory management
- creates

## Management stock inventory

- admin (only) has the permission to update the stock.
- when customer create an order, this doesn't affect direct on the stock inventory, it's only affect after accepting the order.
- admin and employee should be get notified, when the stock is under 400 for example.

## Management for prices

- the price doesn't deleted, instead soft delete, (they flag as not active).
- the price when price updated, the old price marked as not active.
- every product has a different price based on the quantity of order.

    

- Change Data Type For the stock to uint