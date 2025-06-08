import { NavLink } from "react-router-dom";
import { type ReactNode } from "react";
import type { Product } from "../../../app/Models/Product";

export default function ProductCard({
  children,
  product,
}: {
  product: Product;
  children?: ReactNode;
}) {
  return (
    <div className="flex flex-col sm:w-1/3 md:w-1/3 shadow-md/30">
      <img src="/src/assets/solo-leveling.jpg"></img>
      <div className="bg-white rounded-b-md p-2 flex flex-col items-center gap-3">
        <div>
          <NavLink
            className="text-blue-800 font-bold text-xl"
            to={"/productStock/" + product.productId}
          >
            {product.name}
          </NavLink>
        </div>
        <p className=" text-gray-400">
          {product.description.length > 30
            ? product.description.slice(0, 29) + "....."
            : product.description}
        </p>

        {children}
      </div>
    </div>
  );
}
