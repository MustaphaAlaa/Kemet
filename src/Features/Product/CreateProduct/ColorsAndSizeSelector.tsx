import { MdCheckCircle, MdCircle } from "react-icons/md";
import SelectedCircle from "../../../Components/ReuseableComponents/SelectedCircle";



interface colorsAndSizeSelectorArgs {
  allColorsHasSameSize: boolean | null,
  handleSizesSameForColors: () => void,
  handleSizesDifferentForColors: () => void,
}




export default function ColorsAndSizeSelector({ handleSizesSameForColors, allColorsHasSameSize, handleSizesDifferentForColors }: colorsAndSizeSelectorArgs) {
  return <div className="flex flex-row justify-between">
    <div className="flex flex-row justify-between gap-3 items-center">
      <SelectedCircle onClick={(handleSizesSameForColors)}>
        {allColorsHasSameSize ? <MdCheckCircle className="text-rose-500 border-white    " /> : <MdCircle />}
      </SelectedCircle>
      <p>نعم</p>
    </div>

    <div className="flex flex-row justify-between gap-3 items-center">


      <SelectedCircle onClick={(handleSizesDifferentForColors)}>
        {allColorsHasSameSize === null ? <MdCircle /> : !allColorsHasSameSize ? <MdCheckCircle className="text-rose-500 border-white    " /> : <MdCircle />}
      </SelectedCircle>


      <p>لا</p>
    </div>
  </div>;
}
