import ApiLinks from "../../../APICalls/ApiLinks";
import GetData from "../../../APICalls/GetData";
import type { Color } from "../../../app/Models/Color";
import { ColorCircle } from "../../Colors/ColorCircle";

interface ColorCirclesArgs {
  productId: string | undefined;
  setExpanded: React.Dispatch<React.SetStateAction<number | boolean>>;
  selectedColor: number;
  setSelectColor: React.Dispatch<React.SetStateAction<number>>;
}

export default function ProductColorCircles({
  productId,
  setExpanded,
  selectedColor,
  setSelectColor,
}: ColorCirclesArgs) {
  const { response: productVariantColors } = GetData<Color[]>(
    `${ApiLinks.productVariant.details}/${productId}`
  );
  const handleColorCircleClick = (colorId: number) => {
    if (selectedColor == -1) {
      setExpanded(colorId);
      setSelectColor(colorId);
    } else if (colorId == selectedColor) {
      setExpanded(false);
      setSelectColor(-1);
    } else if (selectedColor != -1 && selectedColor != colorId) {
      setSelectColor(colorId);
      setExpanded(false);
      setTimeout(() => setExpanded(colorId), 200);
    }
  };

  const colorCircles = productVariantColors?.result?.map((item) => (
    <ColorCircle
      cursorPointer
      key={item.colorId}
      onClick={() => handleColorCircleClick(item.colorId)}
      color={item}
    ></ColorCircle>
  ));
  return colorCircles;
}



