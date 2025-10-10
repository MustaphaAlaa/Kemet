import { useEffect, useState } from "react";
import type { Size } from "../../../../app/Models/Size";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import type { ProductVariantWithDetails } from "../../../../app/Models/Product/ProductVariantReadWithDetailsDTO";
import axios from "axios";
import UpdateStock from "./UpdateStock";
import { MdOutlineCreate } from "react-icons/md";
import ApiLinks from "../../../../APICalls/ApiLinks";
import LabelPanel from "../../../../Components/ReuseableComponents/LabelPanel";
import { useSelector } from "react-redux";
import { selectUserRoles } from "../../../../../store/store";
import { rolesTypes } from "../../../../app/Auth/roles";
import { privateApi } from "../../../../APICalls/privateApi";

export default function SizeAndStock({
  size,
  selectedColor,
  productVariantId,
}: {
  size: Size;
  selectedColor: number;
  productVariantId: string;
}) {

  const roles: string[] = useSelector(selectUserRoles);
  const isAdmin = roles.includes(rolesTypes.ADMIN);

  const [updateMode, setUpdateMode] = useState(false);
  const handleClick = () => {
    setUpdateMode(!updateMode);
  };

  const [productVariantStock, setProductVariantStock] =
    useState<APIResponse<ProductVariantWithDetails>>();
  const [stockQuantity, setStockQuantity] = useState<number | undefined>();
  // let stockQuantity: number | undefined = 0;
  const getProductStockForSize = async () => {
    const { data }: { data: APIResponse<ProductVariantWithDetails> } = await privateApi.get(
      `${ApiLinks.productVariant.details}/${productVariantId}/${selectedColor}/${size.sizeId}`
    );

    setProductVariantStock(data);

  };
  useEffect(() => {
    getProductStockForSize();
  }, [selectedColor, size.sizeId, productVariantId]);

  console.log(`inside sizeAndStock`);
  if (productVariantStock != undefined && stockQuantity == undefined) {
    setStockQuantity(productVariantStock?.result?.stockQuantity);
  }
  //
  let content = <span className="">{stockQuantity ?? 0}</span>



  if (isAdmin) {
    content = !updateMode ?
      content :
      <UpdateStock
        productVariant={productVariantStock?.result}
        setProductVariant={setProductVariantStock}
        setStock={setStockQuantity}
        setUpdateMode={setUpdateMode}
      ></UpdateStock>   // for admin only
      ;
  }



  return (
    <>
      {productVariantStock ? (
        <div className="w-full   flex flex-col my-1 p-2 justify-between items-center rounded-xl bg-gradient-to-l from-cyan-700 to-sky-800 shadow-md/30 border-indigo-200 border border-3">
          <div className="flex flex-row items-center justify-between w-full">


            <LabelPanel
              className="w-1/3"
              defaultStyle
              label={<span>المقاس</span>}
            >
              <span className="p-3 text-blue-800">{size.name}</span>
            </LabelPanel>



            <LabelPanel
              styleLabel=" flex flex-row justify-between items-center p-1  w-full text-center text-sky-800   font-bold bg-white"
              className=" w-1/3 border-sky-100  items-center bg-sky-300"
              label={
                <>
                  <span className={`${!isAdmin ? 'mx-auto' : ''}`}>المخزون</span>
                  {isAdmin ? <MdOutlineCreate
                    onClick={handleClick}
                    className="cursor-pointer bg-rose-500  rounded-sm  text-white"
                  /> : null}
                </>
              }
            >
              <span>{content}</span>
            </LabelPanel>


          </div>
        </div>
      ) : null}
    </>
  );
}
