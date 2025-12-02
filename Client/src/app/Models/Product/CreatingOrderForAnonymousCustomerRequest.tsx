


export interface CreatingOrderForAnonymousCustomerRequest {
    productQuantityPriceId: number;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    streetAddress: string | null;
    sameLastAddress: boolean;
    governorateId: number;
    productVariantIdsWithQuantity: { [key: number]: number; };
}



/**
 * const order: CreatingOrderForAnonymousCustomerRequest = {
  productQuantityPriceId: 12345,
  firstName: "Laila",
  lastName: "Nasser",
  phoneNumber: "+201001234567",
  streetAddress: "42 Nile Street, Cairo",
  sameLastAddress: false,
  governorateId: 3,

  // The key is the productVariantId, and the value is the quantity
  productVariantIdsWithQuantity: {
    101: 2,   // variant ID 101 → 2 units
    205: 1,   // variant ID 205 → 1 unit
    319: 5,   // variant ID 319 → 5 units
  },
};

 * 
 * 
 */