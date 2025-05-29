import { useState } from "react";
import type { APIResponse } from "../../Models/APIResponse";
import type { Color } from "../../Models/Color";
import InputText from "../../Components/ReuseableComponents/InputText";
import Button from "../../Components/ReuseableComponents/Button";
import { MdSave } from "react-icons/md";
import axios from "axios";
import domain from "../../Models/domain";

export default function EditColor({ closeUpdateMode, color, notifyColorUpdated, colorUpdated }: { closeUpdateMode: any, color: Color, colorUpdated: boolean, notifyColorUpdated: (x: boolean) => void }) {

    const labelsGroup = `flex flex-col md:flex-row  items-center text-center`;

    const [colorName, setColorName] = useState(color.name);
    const [hexacode, setHexacode] = useState(color.hexacode);
    const handleSubmit = async (event) => {
        event.preventDefault();

        const { data }: { data: APIResponse<Color[]> } = await axios.put(`${domain}api/a/Color/`, { ColorId: color.colorId, Name: colorName, HexaCode: hexacode })
        // const { data } = await axios.get(`${domain}api/Color/index`)//, { Name: colorName, HexaCode: hexacode })
        // .then(d => console.log(d))

        console.log(data);

        if (data.statusCode === 200) {
            closeUpdateMode(false);
            notifyColorUpdated(!colorUpdated);
        }
        // setColorName('');
        // setHexacode('');


    }

    return (

        <form onSubmit={handleSubmit} method="put" className=" flex flex-col items-center">
            <div className=" grid md:grid-rows-2  p-3 gap-5 justify-center">
                <div className={`${labelsGroup}`}>
                    <label htmlFor="colorName" className="block mb-2   sm:w-1/4">اسم اللون</label>
                    <InputText name="colorName" onChange={(event) => setColorName(event.target.value)} value={colorName} id="colorName" styles="rounded-md shadow-md/30 border border-2 border-gray-300  " />
                </div>
                <div className={`${labelsGroup}`}>
                    <label htmlFor="hexaCode" className="mb-2 w-1/4">كود اللون</label>
                    <InputText dir='ltr' name="hexaCode" onChange={(event) => setHexacode(event.target.value)} value={hexacode} id="hexaCode" styles="rounded-md shadow-md/30 border border-2 border-gray-300" />
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
