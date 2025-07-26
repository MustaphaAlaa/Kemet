import { useState } from "react";
import { SizeLabel } from "./SizeLabel";
import useSizeContext from "../../hooks/useSizeContext";

export default function SizesList() {
  const { response: sizes } = useSizeContext();

  const [updateModeId, setUpdateModeId] = useState(-1);
  const SizeSorted = sizes?.result?.sort((a, b) => {
    // eslint-disable-next-line no-extra-boolean-cast
    if (!!parseInt(a.name) && !!parseInt(b.name))
      return parseInt(a.name) - parseInt(b.name);
    return a.name.toLocaleLowerCase().localeCompare(b.name.toLocaleLowerCase());
  });

  const sizesLabels = SizeSorted?.map((item) => (
    <SizeLabel
      setUpdateModeId={setUpdateModeId}
      updateModeId={updateModeId}
      key={item.sizeId}
      size={item}
    ></SizeLabel>
  ));

  return (
    <div dir="ltr" className="flex flex-row flex-wrap justify-center">
      {sizesLabels}
    </div>
  );
}
