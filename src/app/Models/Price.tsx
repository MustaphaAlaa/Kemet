







export interface Price {
    priceId: number;
    minimumPrice: number;
    maximumPrice: number;
    startFrom?: string | Date | null;
    endsAt?: string | Date | null;
    createdAt?: string | Date | null;
    updatedAt?: string | Date | null;
    note?: string;
    productId?: string;
    isActive?: boolean;
}
