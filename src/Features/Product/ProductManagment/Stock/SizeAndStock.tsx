import { useEffect, useState } from "react";
import type { Size } from "../../../../app/Models/Size";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import type { ProductVariantWithDetails } from "../../../../app/Models/ProductVariantReadWithDetailsDTO";
import axios from "axios";
import UpdateStock from "./UpdateStock";
import { MdOutlineCreate } from "react-icons/md";
import ApiLinks from "../../../../APICalls/ApiLinks";

export default function SizeAndStock({
  size,
  selectedColor,
  productVariantId,
}: {
  size: Size;
  selectedColor: number;
  productVariantId: string;
}) {
  const [updateMode, setUpdateMode] = useState(false);
  const handleClick = () => {
    setUpdateMode(!updateMode);
  };

  const [productVariantStock, setProductVariantStock] =
    useState<APIResponse<ProductVariantWithDetails>>();
  const [stockQuantity, setStockQuantity] = useState<number | undefined>();
  // let stockQuantity: number | undefined = 0;
  const getProductStockForSize = async () => {
    const { data }: APIResponse<ProductVariantWithDetails> = await axios.get(
      `${ApiLinks.productVariant.details}/${productVariantId}/${selectedColor}/${size.sizeId}`
    );

    let done = false;

    if (data != undefined) {
      setProductVariantStock(data);
      // stockQuantity = productVariantStock?.result?.stockQuantity;
      console.log(data);
      done = true;
    } else {
      console.log(`mmmmmmmmmooooooooooooooooooowFwwwwwwwwwwwwwwwwwwww`);
    }

    const ob = {
      productVariantStock,
      done: true,
    };
    console.log(done ? ob : "something wrong here");
  };
  useEffect(() => {
    // console.log(
    //   `777777debug: pv.id => ${productVariantStock?.result?.productVariantId} color=> ${productVariantStock?.result?.color.colorId} size => ${productVariantStock?.result?.size.sizeId} stock=> ${productVariantStock?.result?.stockQuantity}`
    // );

    getProductStockForSize();
  }, [selectedColor, size.sizeId, productVariantId]);

  console.log(`inside sizeAndStock`);
  if (productVariantStock != undefined && stockQuantity == undefined) {
    setStockQuantity(productVariantStock?.result?.stockQuantity);
  }

  const content = !updateMode ? (
    <span className="">{stockQuantity ?? 0}</span>
  ) : (
    <UpdateStock
      productVariant={productVariantStock.result}
      setStock={setStockQuantity}
      setUpdateMode={setUpdateMode}
    ></UpdateStock>
  );
  // const content = !updateMode ? (
  //   <div className="w-full bg-sky-300 border-white border-3 border-3 text-cyan-800  text-center text-3xl  p-3   rounded-b-md  font-bold  shadow-md/30">
  //     <span className="">{stockQuantity ?? 0}</span>
  //   </div>
  // ) : (
  //   <UpdateStock
  //     productVariant={productVariantStock.result}
  //     setStock={setStockQuantity}
  //     setUpdateMode={setUpdateMode}
  //   ></UpdateStock>
  // );

  return (
    <>
      {productVariantStock ? (
        <div className="w-full   flex flex-col my-1 p-2 justify-between items-center rounded-xl bg-gradient-to-l from-cyan-700 to-sky-800 shadow-md/30 border-indigo-200 border border-3">
          <div className="flex flex-row items-center justify-between w-full">
            {/* <SizeCircle size={size}></SizeCircle> */}
            <div className="flex flex-col border w-1/3 border-3 border-sky-100 font-bold text-xl items-center rounded-lg bg-gray-300 text-white">
              <span className="w-full text-center text-sky-800 p-1  bg-white">
                المقاس
              </span>
              <span className="p-3 text-blue-800">{size.name}</span>
            </div>
            <div className="flex flex-col border w-1/3 border-3 border-sky-100  text-xl items-center rounded-lg bg-sky-300 text-white">
              <div className="flex flex-row justify-between items-center p-1  w-full text-center text-sky-800   font-bold bg-white">
                <span>المخزون</span>
                <MdOutlineCreate
                  onClick={handleClick}
                  className="cursor-pointer bg-rose-500  rounded-sm  text-white"
                />
              </div>
              <span className="p-3 text-blue-800 font-bold">{content}</span>
            </div>

            {/* <div className="text-center w-1/2 font-bold   text-center text-sky-800 p-1 font-bold  ">
              <div className="w-full text-center text-sky-800 p-1 font-bold bg-white ">
                المخزون
              </div>
              {content}
            </div> */}
          </div>
        </div>
      ) : null}
    </>
  );
}
