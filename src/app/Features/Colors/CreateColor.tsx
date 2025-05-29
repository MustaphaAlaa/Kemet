import { MdBrush, MdOutlineAddCircleOutline } from "react-icons/md";
import Button from "../../Components/ReuseableComponents/Button";
import InputText from "../../Components/ReuseableComponents/InputText";
import axios from "axios";
import { useState, type FormEvent } from "react";
import domain from "../../Models/domain";
import type { APIResponse } from "../../Models/APIResponse";
import type { Color } from "../../Models/Color";
import useColorsContext from "../../../hooks/useColorsContext";


export default function CreateColor() {

    const { setColorIsAdded, isColorAdded } = useColorsContext();

    const [colorName, setColorName] = useState('');
    const [hexacode, setHexacode] = useState('');
    
    const labelsGroup = `flex flex-col md:flex-row  items-center text-center`;
    

    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        const { data }: { data: APIResponse<Color[]> } = await axios.post(`${domain}api/a/Color/add`, { Name: colorName, HexaCode: hexacode })

        console.log(data);

        if (data.statusCode === 201)
            setColorIsAdded(!isColorAdded);


        setColorName('');
        setHexacode('');
    }

    return (
        <div className="bg-gray-100 grid auto-rows-min gap-4 p-4">
            <h2 className="text-3xl flex items-center gap-2 mx-auto mb-8">
                إضافة لون جديد <MdBrush />
            </h2>

            <form onSubmit={handleSubmit} method="post" className="flex flex-col items-center">
                <div className="grid md:grid-rows-2  p-3 gap-5 justify-center">
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
                    <Button success outline hover roundedLg>
                        اضف اللون <MdOutlineAddCircleOutline className="text-xl ml-1" />
                    </Button>
                </div>
            </form>
        </div>
    );
}
