import { MdBrush, MdOutlineAddCircleOutline, MdOutlineClose } from "react-icons/md";
import Button from "../../Components/ReuseableComponents/Button";
import InputText from "../../Components/ReuseableComponents/InputText";
import { useState, type FormEvent } from "react";
import useColorsContext from "../../hooks/useColorsContext";
import Portal from "../../Components/ReuseableComponents/Portal";
import ApiLinks from "../../APICalls/ApiLinks";


export default function CreateColor({ handleClose }: { handleClose: () => void }) {

    const { createEntity: createColor } = useColorsContext();

    const [colorName, setColorName] = useState('');
    const [hexacode, setHexacode] = useState('');

    const labelsGroup = `flex flex-col  items-center text-center`;


    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        createColor(`${ApiLinks.color.create}`, { Name: colorName, HexaCode: hexacode });

        handleClose();
    }


    const createColorPortalChild = <div className="p-4 items-center flex flex-row items-start">
        <div className="">
            <MdOutlineClose onClick={handleClose} className="hover:-translate-y-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 cursor-pointer text-2xl text-rose-500 bg-white shadow rounded-md" />
        </div>
        <div className="pt-5 text-3xl  flex flex-row gap-2 mx-auto mb-2   justify-center">
            <h2 className="font-bold text-transparent bg-gradient-to-r from-blue-600 via-rose-500 to-cyan-500 bg-clip-text   border-b border-b-2 pb-3 border-gray-300">
                إضافة لون جديد
            </h2>

            <MdBrush className="" />

        </div>
    </div>

    const actionBar = <form onSubmit={handleSubmit} method="post" className="flex flex-col items-center justify-center">
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
            <Button className="flex flex-row justify-between w-32 " primary outline hover roundedLg>
                اضف اللون <MdOutlineAddCircleOutline className="text-xl ml-1" />
            </Button>
        </div>
    </form>

    return <Portal style={`bg-gradient-to-r from-pink-100 to-sky-300 shadow-xl rounded-3xl`} handleClose={handleClose} actionBar={actionBar}>
        {createColorPortalChild}
    </Portal>

}
