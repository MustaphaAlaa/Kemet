import { useEffect, useState } from "react";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import type { ProductVariantWithDetails } from "../../../../app/Models/ProductVariantReadWithDetailsDTO";
import axios from "axios";
import SizeAndStock from "./SizeAndStock";
import ApiLinks from "../../../../APICalls/ApiLinks";
import type { Color } from "../../../../app/Models/Color";
import GetData from "../../../../APICalls/GetData";

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
    <div className="w-full flex flex-col items-center space-y-10">
  
       <ColorSpan colorId={selectedColor}></ColorSpan>
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



export function ColorSpan({ colorId }: { colorId: number | string }) {

  const { data: color } = GetData<Color>(`${ApiLinks.color.getColor}/${colorId}`);

  return <div
    style={{ backgroundColor: `${color?.hexacode}` }}
    className={`shadow-md/50   rounded-full h-15 w-15 border-3 border-white`}
  ></div>

}