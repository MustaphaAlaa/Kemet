import { useState } from "react";
import GetData from "../../../APICalls/GetData";
import type { Category } from "../../../app/Models/Category";
import type { Color } from "../../../app/Models/Color";
import type { Size } from "../../../app/Models/Size";
import ApiLinks from "../../../APICalls/ApiLinks";

export function useProductFormData() {
  const { response: categories } = GetData<Category[]>(
    ApiLinks.category.getAll
  );
  const { response: colors } = GetData<Color[]>(ApiLinks.color.getAll);
  const { response: sizes } = GetData<Size[]>(ApiLinks.size.getAll);

  const [selectedColors, setSelectedColors] = useState<number[]>([]);
  const [selectedSizes, setSelectedSizes] = useState<number[]>([]);
  const [colorWitItsSizes, setColorWithItsSizes] =
    useState<{ colorId: number; sizes: number[] }[]>();

  return {
    categories,
    colors,
    sizes,
    selectedColors,
    setSelectedColors,
    selectedSizes,
    setSelectedSizes,
    colorWitItsSizes,
    setColorWithItsSizes,
  };
}
