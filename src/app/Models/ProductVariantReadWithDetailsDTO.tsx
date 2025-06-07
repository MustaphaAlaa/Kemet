import type { Color } from "./Color";
import type { Size } from "./Size";




export interface ProductVariantWithDetails {
    productVariantId: number;
    productId: number;
    color: Color;
    size: Size;
    stock: number;
}
