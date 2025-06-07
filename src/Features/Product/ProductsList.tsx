import { useEffect } from "react";
import type { APIResponse } from "../../app/Models/APIResponse";
import type { Product } from "../../app/Models/Product";
import ProductActionButton from "./ProductCard/ProductActionButton";
import ProductCard from "./ProductCard/ProductCard";



export function ProductsList({ getProducts, response }: { getProducts: () => Promise<void>; response: APIResponse<Product[]> | undefined; }) {

    useEffect(() => {
        getProducts();
    }, [getProducts]);
    console.log(response?.result);
    if (!response?.result) return <p>Loading products...</p>;

    const admin = false;

    const productsItems = response?.result?.map(item => {
        if (!admin)
            return <ProductActionButton product={item} key={item.productId}></ProductActionButton>;
        return <ProductCard product={item} key={item.productId}></ProductCard>;
    });

    return productsItems;

}
