import { useState, type FormEvent } from "react";
import type { Color } from "../../Models/Color";
import InputText from "../../Components/ReuseableComponents/InputText";
import Button from "../../Components/ReuseableComponents/Button";
import { MdSave } from "react-icons/md";
import useColorsContext from "../../../hooks/useColorsContext";

export default function EditColor({ closeUpdateMode, color }: { closeUpdateMode: any, color: Color }) {

    const { updateColor } = useColorsContext();

    const [colorName, setColorName] = useState(color.name);
    const [hexacode, setHexacode] = useState(color.hexacode);

    const labelsGroup = `flex flex-col md:flex-row  items-center text-center`;


    const handleSubmit = (event: FormEvent) => {
        event.preventDefault();

        updateColor({ colorId: color.colorId, colorName, hexacode });
        closeUpdateMode(false);

    }


    return (

        <form onSubmit={handleSubmit} method="put" className=" flex flex-col items-center">
            <div className=" grid md:grid-rows-2  p-3 gap-5 justify-center">
                <div className={`${labelsGroup}`}>
                    <label htmlFor="colorName" className="block mb-2   sm:w-1/4">اسم اللون</label>
                    <InputText name="colorName" onChange={(event) => setColorName(event.target.value)} value={colorName} id="colorName" styles="text-center  rounded-md shadow-md/30 border border-2 border-gray-300  " />
                </div>
                <div className={`${labelsGroup}`}>
                    <label htmlFor="hexaCode" className="mb-2 w-1/4">كود اللون</label>
                    <InputText dir='ltr' name="hexaCode" onChange={(event) => setHexacode(event.target.value)} value={hexacode} id="hexaCode" styles="text-center  rounded-md shadow-md/30 border border-2 border-gray-300" />
                </div>
            </div>

            <div className="md:justify-self-center md:my-auto">
                <Button primary outline hover roundedLg>
                    حفظ اللون <MdSave className="text-xl ml-1" />
                </Button>
            </div>
        </form>
    );
}
