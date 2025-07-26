import type { QuantityPrices } from "./QuantityPrices";

export interface ProductPriceOrchestratorDTO {
    priceId: number;
    minimumPrice: number;
    maximumPrice: number;
    quantitiesPrices: QuantityPrices[];
    productId: number;
}
