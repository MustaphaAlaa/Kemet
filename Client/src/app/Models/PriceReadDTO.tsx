



export interface PriceReadDTO {
    priceId: number;
    minimumPrice: number;
    maximumPrice: number;
    startFrom: Date | string | null;
    endsAt: Date | string | null;
    note: string | null;
    isActive: boolean;
    createdAt: Date | string | null;
}
