import { useEffect, useState, type FormEvent, type ReactNode } from "react";
import axios from "axios";
import type { APIResponse } from "../../../app/Models/APIResponse";
import { ColorCircle } from "../../Colors/ColorCircle";
import type { ProductWithVariantsCreateDTO } from "../../../app/Models/Product/ProductWithVariantsCreateDTO";
import { useNavigate } from "react-router-dom";
import SizeComponent from "../../../Components/ReuseableComponents/SizeComponent";
import { useProductFormData } from "./useProductFormData";
import { CreateProductForm } from "./CreateProductForm";
import { ColorsSpan } from "../../../Components/ReuseableComponents/Colors/ColorsSpan";
import ApiLinks from "../../../APICalls/ApiLinks";

export default function CreateProduct() {
  //entities data
  const {
    categories,
    colors,
    sizes,
    selectedColors,
    setSelectedColors,
    selectedSizes,
    setSelectedSizes,
    colorWitItsSizes,
    setColorWithItsSizes,
  } = useProductFormData();




  const navigate = useNavigate();

  let dangerSpan: ReactNode;

  const [productName, setProductName] = useState("");
  const [productDescription, setProductDescription] = useState("");
  const [categoryId, setCategoryValue] = useState<number>(0);
  const [allColorsHasSameSize, setAllColorHasSameSize] = useState<
    boolean | null
  >(null);
  const [
    sizeComponentAndColorsSizeComponent,
    setSizeComponentAndColorsSizeComponent,
  ] = useState<ReactNode | null>(null);

  const handleSizesSameForColors = () => {
    setAllColorHasSameSize(true);
    const sizeComponent = (
      <SizeComponent
        assignSelectedSizes={setSelectedSizes}
        sizes={sizes?.result}
        label={
          <h3 className="text-xl text-white text-shadow  font-bold">
            المقاسات المتاحة للمنتج
          </h3>
        }
      ></SizeComponent>
    );
    setSizeComponentAndColorsSizeComponent(sizeComponent);
  };

  const handleSizesDifferentForColors = () => setAllColorHasSameSize(false);

  const colorWithItzSizes: { colorId: number; sizes: number[] }[] = [];

  useEffect(() => {
    if (!allColorsHasSameSize && allColorsHasSameSize !== null) {
      const sizeAndColors = selectedColors?.map((item) => {
        const color = colors?.result?.find((el) => el.colorId === item);

        if (color === undefined)
          throw new Error("Cannot deal with undefined color");

        const colorCircle = <ColorCircle color={color}></ColorCircle>;

        return (
          <SizeComponent
            key={color.colorId}
            arm={colorWithItzSizes}
            colorId={color.colorId}
            assignSelectedSizes={setSelectedSizes}
            sizes={sizes?.result}
            label={colorCircle}
          ></SizeComponent>
        );
      });

      setSizeComponentAndColorsSizeComponent(sizeAndColors);

      setColorWithItsSizes(colorWithItzSizes);
    }
  }, [allColorsHasSameSize, selectedColors, selectedSizes]);

  const categoryOptions = categories?.result?.map((item) => {
    return (
      <option title={item.name} key={item.categoryId} value={item.categoryId}>
        {item.name}
      </option>
    );
  });

  const colorSpans = (
    <ColorsSpan
      setSelectedColors={setSelectedColors}
      colors={colors?.result}
    ></ColorsSpan>
  );

  const productWithVariantsCreateDTO: ProductWithVariantsCreateDTO = {
    Name: productName,
    description: productDescription,
    CategoryId: categoryId,
    ColorsIds: selectedColors.sort((a, b) => a - b),
    AllColorsHasSameSizes: allColorsHasSameSize,
    SizesIds: allColorsHasSameSize ? selectedSizes.sort((a, b) => a - b) : [],
    ColorsWithItSizes:
      allColorsHasSameSize === false
        ? Object.fromEntries(
          (colorWitItsSizes ?? []).map((item) => [
            item.colorId,
            item.sizes.sort((a, b) => a - b),
          ])
        )
        : null,
  };


  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault();
    if (categoryId == 0) {
          console.log('Cannot create product to null category')
    } else {

      const { data }: { data: APIResponse<boolean> } = await axios.post(
        `${ApiLinks.product.create}`,
        productWithVariantsCreateDTO
      );

      if (data.statusCode == 201) navigate(`/createOrder`);

    }
  };

  const onChangeCategory = (event) => {
    setCategoryValue(event.target.value);
  };

  return (
    <CreateProductForm
      handleSubmit={handleSubmit}
      setProductName={setProductName}
      setProductDescription={setProductDescription}
      onChangeCategory={onChangeCategory}
      categoryId={categoryId}
      categoryOptions={categoryOptions}
      colorSpans={colorSpans}
      handleSizesSameForColors={handleSizesSameForColors}
      allColorsHasSameSize={allColorsHasSameSize}
      handleSizesDifferentForColors={handleSizesDifferentForColors}
      sizeComponentAndColorsSizeComponent={sizeComponentAndColorsSizeComponent}
    ></CreateProductForm>
  );
}
