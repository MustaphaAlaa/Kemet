export interface ProductQuantityPriceReadDTO {
    productQuantityPriceId: number,
    productId: number,
    quantity: number,
    unitPrice: number,
    isActive: boolean,
    note: string | null,
    createdAt: Date,
}



