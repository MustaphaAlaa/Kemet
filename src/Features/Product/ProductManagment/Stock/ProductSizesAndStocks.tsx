import { useEffect, useState } from "react";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import type { ProductVariantWithDetails } from "../../../../app/Models/ProductVariantReadWithDetailsDTO";
import axios from "axios";
import SizeAndStock from "./SizeAndStock";
import ApiLinks from "../../../../APICalls/ApiLinks";

interface ProductSizesAndStocksArgs {
  productId: string;
  selectedColor: number;
}

export default function ProductSizesAndStocks({
  productId,
  selectedColor,
}: ProductSizesAndStocksArgs) {
  const [productVariantSizes, setProductVariantSize] =
    useState<APIResponse<ProductVariantWithDetails[]>>();

  const getProductSizes = async () => {
    const { data } = await axios.get(
      `${ApiLinks.productVariant.endpoint}/${productId}/${selectedColor}`
    );

    setProductVariantSize(data);
  };

  useEffect(() => {
    getProductSizes();
  }, [selectedColor, productId]);

  console.log(
    `ProductSizesAndStocks ===== PID ${productId} ,,,, SelectedColor ${selectedColor}`
  );

  const sizes = productVariantSizes?.result?.map((item) => item.size);

  const labelStyle = "text-2xl text-white font-bold";

  return (
    <div className="w-full flex flex-col">
      {/* <div className="flex flex-row justify-between items-center ">
        <h1 className={`${labelStyle}`}>المقاسات</h1>
        <h1 className={`${labelStyle} ml-8`}>المخزون</h1>
      </div> */}

      {sizes?.map((item) => {
        return (
          <SizeAndStock
            size={item}
            selectedColor={selectedColor}
            productVariantId={productId}
            key={item.sizeId}
          ></SizeAndStock>
        );
      })}
    </div>
  );
}
