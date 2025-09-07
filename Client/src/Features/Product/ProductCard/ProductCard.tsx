import { NavLink } from "react-router-dom";
import { type ReactNode } from "react";
import type { Product } from "../../../app/Models/Product/Product";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";
import CustomerForm from "../../CustomerForm";
import { useRoles } from "../../../hooks/useRoles";
export default function ProductCard({
  children,
  product,
}: {
  product: Product;
  children?: ReactNode;
}) {
  const { isAdmin, isEmployee } = useRoles();

  return (
    <div className="flex flex-col sm:w-1/3 md:w-1/3 shadow-md/30">
      <img src="/src/assets/solo-leveling.jpg"></img>
      <div className="bg-white rounded-b-md p-2 flex flex-col items-center gap-3">
        <div>
          {isAdmin || isEmployee ? <NavLink
            className="text-blue-800 font-bold text-xl"
            to={`${NavigationLinks.product.page}/${product.productId}`}
            state={{ product }}
          >
            {product.name}
          </NavLink> : <NavLink
            className="text-blue-800 font-bold text-xl"
            to={`/createorder`}
            state={{ product }}
          >
            {product.name}
          </NavLink>}
        </div>
        <p className=" text-gray-400">
          {product.description.length > 30
            ? product.description.slice(0, 29) + "....."
            : product.description}
        </p>
        {children}
      </div>
    </div >
  );
}
