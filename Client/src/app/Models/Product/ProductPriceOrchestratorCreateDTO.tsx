import type { QuantityPrices } from "./QuantityPrices";

export interface ProductPriceOrchestratorCreateDTO {
    minimumPrice: number;
    maximumPrice: number;
    startFrom?: Date;
    EndsAt?: Date;
    Note?: string;
    quantitiesPrices: QuantityPrices[];

    productId: number;
}
