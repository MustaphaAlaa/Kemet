import { NavLink } from "react-router-dom";
import Button from "../../../Components/ReuseableComponents/Button";
import type { Product } from "../../../app/Models/Product/Product";
import { FaDollarSign } from "react-icons/fa6";
import { MdOutlineInventory2 } from "react-icons/md";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";
import { AiOutlineSnippets } from "react-icons/ai";
import { useRoles } from "../../../hooks/useRoles";




export default function ProductActionButton({ product }: { product: Product }) {
    const { isAdmin, isEmployee } = useRoles();
    console.log('is Employee?', isEmployee);
    console.log('is Admin?', isAdmin);



    const btnStyle = 'shadow-md/20 flex flex-row gap-3 items-center hover:font-bold'
    return <>
        <div className="flex flex-col md:flex-row justify-center items-center">
            {isAdmin ? <>
                <NavLink to={`${NavigationLinks.product.productPrice}/${product.productId}`} state={{ product }}>
                    <Button styles={btnStyle} roundedLg hover primary>
                        إدارة اسعار المنتج
                        <span><FaDollarSign className="text-xl" /> </span></Button>
                </NavLink>
            </>
                : null}

            {isEmployee ?

                <NavLink to={`${NavigationLinks.product.productStock}/${product.productId}`} state={{ product }}>
                    <Button styles={btnStyle} roundedLg hover warning>
                        إدارة مخزون المنتج
                        <span><MdOutlineInventory2></MdOutlineInventory2></span> </Button>
                </NavLink>

                : null}

            <NavLink to={`${NavigationLinks.product.orders}/${product.productId}`} state={{ product }}>
                <Button styles={btnStyle} roundedLg hover primary outline>
                    الطلبات
                    <span><AiOutlineSnippets className="text-xl" /> </span></Button>
            </NavLink>
        </div>
    </>
}
