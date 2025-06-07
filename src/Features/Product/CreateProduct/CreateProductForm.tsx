import { InputTextValChange } from "../../../Components/ReuseableComponents/InputText";
import Button from "../../../Components/ReuseableComponents/Button";
import type { FormEvent, JSX, ReactNode } from "react";
import ColorsAndSizeSelector from "./ColorsAndSizeSelector";


interface createProductFormArgs {
  categoryId: number,
  allColorsHasSameSize: boolean | null,
  sizeComponentAndColorsSizeComponent: ReactNode
  setProductName: React.Dispatch<React.SetStateAction<string>>,
  setProductDescription: React.Dispatch<React.SetStateAction<string>>,
  categoryOptions: JSX.Element[] | undefined,
  colorSpans: JSX.Element ,
  onChangeCategory: (event: any) => void,
  handleSubmit: (event: FormEvent<Element>) => Promise<void>,
  handleSizesSameForColors: () => void,
  handleSizesDifferentForColors: () => void,
}


export function CreateProductForm({ handleSubmit, setProductName,
  setProductDescription, onChangeCategory, categoryId, categoryOptions,
  colorSpans, handleSizesSameForColors, allColorsHasSameSize, handleSizesDifferentForColors,
  sizeComponentAndColorsSizeComponent }: createProductFormArgs) {


  const mobileFormGroup = "flex flex-col";
  const mdFormGroup = " md:flex-row";
  const xlFormGroup = " justify-center gap-5 ";



  const inputTextStyle = "placeholder-cyan-800 rounded-md shadow-md  flex-1";

  const formGroup = ` ${mobileFormGroup} ${mdFormGroup} ${xlFormGroup} items-center`;

  return <div className="mx-auto mt-10 w-1/2">
    <form method="post" onSubmit={handleSubmit} className="flex flex-col gap-3 items-center justify-center">
      <div className={`${formGroup}`}>
        <InputTextValChange setTextValue={setProductName} placeholder='اسم المنتج' styles={inputTextStyle}></InputTextValChange>
      </div>
      <div className={`${formGroup}`}>
        <InputTextValChange setTextValue={setProductDescription} placeholder="وصف المنتج" styles={inputTextStyle}></InputTextValChange>
        {/* <textarea setTextValue={setProductDescription} placeholder="وصف المنتج" className={`${inputTextStyle} text-blue-800 bg-white w-100 h-64 p-4`} ></textarea> */}
      </div>
      <div className={`${formGroup}`}>
        <select onChange={onChangeCategory} value={categoryId} name="categoryValue" className="bg-white text-blue-800 p-2 shadow rounded-md cursor-pointer">
          <option defaultValue={0} value={'0'}>--التصنيف--</option>
          {categoryOptions}
        </select>
      </div>

      <div className={`flex flex-col items-center p-3 rounded justify-between   bg-gradient-to-r from-teal-500 to-sky-500 shadow-xl rounded-3xl border-3 border-white `}>
        <h3 className="text-xl text-white text-shadow  font-bold">الالوان المتاحة للمنتج</h3>
        <div className="flex flex-row flex-wrap gap-2 p-2 ">
          {colorSpans}
        </div>
      </div>

      <div className={`flex flex-col   rounded   gap-9 p-3 bg-gradient-to-r from-teal-500 to-sky-500 shadow-xl rounded-3xl border-3 border-white `}>
        <h3 className="text-xl text-white text-shadow  font-bold ">المقاسات متاحه بالتساوي لجميع الالوان</h3>

        <ColorsAndSizeSelector handleSizesSameForColors={handleSizesSameForColors} allColorsHasSameSize={allColorsHasSameSize} handleSizesDifferentForColors={handleSizesDifferentForColors} ></ColorsAndSizeSelector>

        {sizeComponentAndColorsSizeComponent}

      </div>

      <Button success hover> <span className="text-2xl font-bold">
        أضف المنتج
      </span>
      </Button>

    </form>
  </div>;
}


