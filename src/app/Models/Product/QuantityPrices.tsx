export interface QuantityPrices {
    quantity: number;
    productQuantityPrice: {
        quantityPrice: number;
        note?: string;
        createAt?: Date;
    };
}
