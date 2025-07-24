import type { Color } from "../../../app/Models/Color";
import ColorSpan from "./ColorSpan";

export function ColorsSpan({
  colors,
  setSelectedColors,
 
}: {
  colors: Color[] | undefined;
 
  setSelectedColors?: React.Dispatch<React.SetStateAction<number[]>>;
}) {
  return colors?.map((item) => (
    <ColorSpan
      selectedColors={setSelectedColors}
 
      key={item.colorId}
      color={item}
    ></ColorSpan>
  ));
}
