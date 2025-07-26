import { useState } from "react";
import { SizeSpan } from "./Colors/ColorSpan";

export  default function SizeComponent({ label, sizes, arm, colorId, assignSelectedSizes }: { colorId?: number, arm?: { colorId: number, sizes: number[] }[], sizes?: Size[], label: ReactNode, assignSelectedSizes: (value: React.SetStateAction<number[]>) => void }) {

  /**
   * i need every component to be have it's own sizes, and send those sizes outside the component to whom interested in the sizes
   *      there are 2 only interested in the sizes:
   *         1- color: size[] one object with many props as needed.
   *         2- size[] (only one array)
   *  */



  const [selectedSizes, setSelectedSizes] = useState<number[]>([]);
  assignSelectedSizes(selectedSizes);


  if (arm !== null || arm !== undefined) {
    const isFound = arm?.findIndex(item => {
      return item.colorId == colorId
    })

    if (isFound !== undefined && isFound >= 0) {
      arm[isFound] = { colorId: colorId ?? 0, sizes: selectedSizes.sort((a, b) => a - b) }
     
    } else {
      arm?.push({ colorId: colorId ?? 0, sizes: selectedSizes.sort((a, b) => a - b) })
    }
    console.log('arm ' + arm);
    console.log(arm);

  }

  const allSizes = sizes?.map(item =>
    <SizeSpan assignSelectedSizes={assignSelectedSizes} setSelectedSizes={setSelectedSizes} key={item.sizeId} size={item}></SizeSpan>
  );


  return <div className={`flex flex-col items-center p-3 rounded justify-between   bg-gradient-to-r from-sky-600 to-teal-500 shadow-xl/30 rounded-3xl border-3 border-white `}>
    {label}
    <div className="flex flex-row flex-wrap gap-2 p-2 justify-between ">
      {allSizes}
    </div>
  </div >
}
