import { useState } from "react";
import type { Color } from "../../../app/Models/Color";
import { MdCheckCircle } from "react-icons/md";
import type { Size } from "../../../app/Models/Size";
import { SizeCircle } from "../../../Features/Sizes/SizeCircle";
import { ColorCircle } from "../../../Features/Colors/ColorCircle";

export default function ColorSpan({
  color,
  selectedColors,
}: {
  color: Color;
  selectedColors?: React.Dispatch<React.SetStateAction<number[]>>;
}) {
  const [selectColor, setSelectColor] = useState(false);

  const handleSpanColorClick = () => {
    if (selectColor) {
      setSelectColor(false);
      // const filterSelectedColors = selectColor
      if (selectedColors)
        selectedColors((prev) => prev.filter((item) => item != color.colorId));
    } else {
      setSelectColor(true);

      if (selectedColors) selectedColors((prev) => [...prev, color.colorId]);
    }
  };

  const divv = selectColor && (
    <span className="absolute inset-2    text-red-400  rounded-full text-xl  mt-1 ">
      <MdCheckCircle className="mx-auto rounded-full shadow-sm/80  "></MdCheckCircle>
    </span>
  );

  return (
    <div onClick={handleSpanColorClick} className="cursor-pointer  relative">
      {/* <div style={{ backgroundColor: `${color.hexacode}` }} className=" shadow-md/50 p-5 rounded-full   border border-3 border-white"></div> */}
      <ColorCircle color={color}></ColorCircle>
      {divv}
    </div>
  );
}

export function SizeSpan({
  assignSelectedSizes,
  size,
  setSelectedSizes,
  selectedSizes,
}: {
  assignSelectedSizes: (value: React.SetStateAction<number[]>) => void;
  size: Size;
  selectedSizes?: number[];
  setSelectedSizes: React.Dispatch<React.SetStateAction<number[]>>;
}) {
  const [selectSize, setSelectSize] = useState(false);

  //iterate in all selected sizes and use the State for what's selected

  // selectedSizes.map(item => {
  //     if (selectedSizes.find(i => i === item))
  //         setSelectSize(true);
  // })

  const handleSpanColorClick = () => {
    if (selectSize) {
      setSelectSize(false);
      setSelectedSizes((prev) => prev.filter((item) => item != size.sizeId));
    } else {
      setSelectSize(true);

      setSelectedSizes((prev) => [...prev, size.sizeId]);
    }
  };

  const divv = selectSize && (
    <span className="absolute inset-7  text-red-400  rounded-full text-xl  mt-1 ">
      <MdCheckCircle className="mx-auto rounded-full shadow-sm/80  "></MdCheckCircle>
    </span>
  );

  const ff = (
    <div onClick={handleSpanColorClick} className="cursor-pointer relative">
      <SizeCircle size={size}></SizeCircle>
      {divv}
    </div>
  );

  return ff;
}
