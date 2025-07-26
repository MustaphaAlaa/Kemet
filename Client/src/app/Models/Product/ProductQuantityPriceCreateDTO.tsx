export interface ProductQuantityPriceCreateDTO {
    quantity: number;
    productId: number ;
    unitPrice: number;
    isActive: boolean;
    note: string | null;
}
