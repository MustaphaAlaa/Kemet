import { useCallback, useState } from "react";
import type { Product } from "../../app/Models/Product";
import type { APIResponse } from "../../app/Models/APIResponse";
import axios from "axios";
import { ProductsList } from "./ProductsList";
import { NavLink } from "react-router-dom";
import Button from "../../Components/ReuseableComponents/Button";
import { MdAddCircle } from "react-icons/md";
import ApiLinks from "../../APICalls/ApiLinks";

export default function ProductPage() {
  const [response, setResponse] = useState<APIResponse<Product[]>>();

  const getProducts = useCallback(async () => {
    const { data } = await axios.get(`${ApiLinks.product.get}`);
    setResponse(data);
  }, []);

  return (
    <div className="flex flex-col    gap-5 items-center justify-center my-8">
      <div>
        <NavLink to="/m/createProduct">
          <Button success hover styles="flex flex-row gap-6 text-2xl">
            أضف منتج
            <span>
              <MdAddCircle />
            </span>
          </Button>
        </NavLink>
      </div>
      <div className="flex flex-col sm:flex-wrap sm:flex-row gap-5 items-center justify-center">
        <ProductsList
          getProducts={getProducts}
          response={response}
        ></ProductsList>
      </div>
    </div>
  );
}
