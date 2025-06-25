import { NavLink } from "react-router-dom";
import Button from "../../../Components/ReuseableComponents/Button";
import ProductCard from "./ProductCard";
import type { Product } from "../../../app/Models/Product";
import { FaDollarSign } from "react-icons/fa6";
import { MdOutlineInventory2 } from "react-icons/md";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";




export default function ProductActionButton({ product }: { product: Product }) {

    const btnStyle = 'shadow-md/20 flex flex-row gap-3 items-center hover:font-bold'
    return <ProductCard product={product}>
        <div className="flex flex-row">
            <NavLink to={`${NavigationLinks.product.productStock}/${product.productId}`} state={{ product }}>

                <Button styles={btnStyle} roundedLg hover warning>
                    إدارة مخزون المنتج
                    <span><MdOutlineInventory2></MdOutlineInventory2></span> </Button>
            </NavLink>
            <NavLink to={`${NavigationLinks.product.productPrice}/${product.productId}`} state={{product}}>
                <Button styles={btnStyle} roundedLg hover primary>
                    إدارة اسعار المنتج
                    <span><FaDollarSign className="text-xl" /> </span></Button>
            </NavLink>
        </div>
    </ProductCard>;
}
