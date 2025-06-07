import { useEffect, useState } from "react";
import type { Size } from "../../../../app/Models/Size";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import type { ProductVariantWithDetails } from "../../../../app/Models/ProductVariantReadWithDetailsDTO";
import axios from "axios";
import { SizeCircle } from "../../../Sizes/SizeCircle";
import { ApiLinks } from "../../../../APICalls/ApiLinks";

export default function SizeAndStock({
  size,
  selectedColor,
  productId,
}: {
  size: Size;
  selectedColor: number;
  productId: string;
}) {
  const [productVariantStock, setProductVariantStock] =
    useState<APIResponse<ProductVariantWithDetails>>();

  const getProductStockForSize = async () => {
    const { data } = await axios.get(
      `${ApiLinks.productVariantDetails}/${productId}/${selectedColor}/${size.sizeId}`
    );

    setProductVariantStock(data);
  };

  useEffect(() => {
    getProductStockForSize();
  }, [selectedColor, size]);

  return (
    <>
      {productVariantStock ? (
        <div className="w-full   flex flex-row my-1 p-2 justify-between items-center rounded-xl bg-gradient-to-l from-cyan-700 to-sky-800 shadow-md/30 border-indigo-200 border border-3">
          <SizeCircle size={size}></SizeCircle>
          <div className="bg-indigo-100 text-center p-4 text-xl rounded-md w-1/4   shadow-md/30">
            {productVariantStock?.result?.stock}
          </div>
        </div>
      ) : null}
    </>
  );
}
