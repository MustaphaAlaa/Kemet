# Price Management

- the price doesn't deleted, instead soft delete, (they flag as not active).
- the price when price updated, the old price marked as not active.
- every product has a different price based on the quantity of order.

## Implementation

- ### Backend

  - Before create a new price, check if the there price exist for the product, if not response with null response.
  - Create PriceOrchestraDTO contains Price & PriceQPrice Data for active (default).
  - for history /product/price/h/product id.
  - when price update it should also checking currently active product quantity if it's not in price range deactivate it.
  -

- ### Frontend

  - if there's no price exist for the product, button for add new button appears
  - the landing page has the last active prices

## Enchantments

- ### Frontend Enchantments

- !!!!!! The code now has issue in PriceRange Components it fetch the price in the price page, and when update PriceRanges button is clicked it's fetch the prices again.

- ### Backend Enchantments
