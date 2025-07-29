# Bugs

- Colors Service => new color cannot be added with name or hexacode values that is already exist,
  but when color updated, it can be add color name or hexa code tha's already exist.

- When creating an order request, GovernorateDeliveryCost is null it should be have foreign key to the latest active governorateDeliveryCost for governorate provided

- When Order approved should be subtracted from the stock, that's not happen.
  
- When customer request an order it's should be valid the quantity if it's not equal to quantity in productQuantityPrice it's Should fail
