import { useState, type ChangeEvent } from "react";
import type { ProductVariantWithDetails } from "../../../../app/Models/ProductVariantReadWithDetailsDTO";
import axios from "axios";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import ApiLinks from "../../../../APICalls/ApiLinks";

export default function UpdateStock({
  productVariant,
  setStock,
  setUpdateMode,
}: {
  productVariant: ProductVariantWithDetails | undefined;
  setStock: React.Dispatch<React.SetStateAction<number | undefined>>;
  setUpdateMode: React.Dispatch<React.SetStateAction<boolean>>;
}) {
  const [value, setValue] = useState<number | undefined>(
    productVariant?.stockQuantity
  );
  if (productVariant == undefined) return null;

  console.log(productVariant);
  console.log("UpdateSTock TTT");

  const handelChange = (event: ChangeEvent<HTMLInputElement>): void => {
    const targetValue = event.target.value;
    const regex = /^\d*$/;
    if (regex.test(targetValue)) {
      setValue(targetValue);
    }
  };

  const update = async () => {
    const { data }: { data: APIResponse<ProductVariantWithDetails> } =
      await axios.put(
        `${ApiLinks.productVariant.stock}/api/productVariant/stock/${productVariant.productVariantId}`,
        value,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
    console.log(`UpdateStock DATA --=`);
    console.log(data);
    setStock(data.result?.stockQuantity);
    setValue(undefined);
  };

  const handleSubmit = (event) => {
    event.preventDefault();

    if (value == undefined) console.log(`value undefined`);
    else {
      update();
      setUpdateMode(false);
    }
  };

  return (
    <form className="w-full" onSubmit={handleSubmit}>
      <input
        type="number"
        value={value}
        onChange={handelChange}
        className="text-center w-full p-4 text-xl rounded-md shadow-md/30 bg-white border border-3 border-blue-100 text-blue-800 font-bold 
        focus:outline
        focus:ring-2
        focus:ring-sky-500"
      />
    </form>
  );
}
