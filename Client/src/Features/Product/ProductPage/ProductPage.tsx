import { useLocation } from "react-router-dom";
import type { Product } from "../../../app/Models/Product/Product"; 
import ProductActionButton from "./ProductActionButton";
import SearchOrder from "./Order/SearchOrder";

export default function ProductPage() {
    const location = useLocation();

    const product: Product = location.state.product;

    return (
        <div className="flex flex-col items-center justify-center p-4">
            <div className="flex flex-col items-center">
                <h1 className="text-2xl font-bold mt-4">{product.name}</h1>
                <p className="text-gray-600 mt-2">{product.description}</p>
            </div>
           <div className="">
             <ProductActionButton product={product}></ProductActionButton>
             <SearchOrder></SearchOrder>
           </div>
        </div>
    )
}
