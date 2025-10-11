import {  MdAddBox,  MdOutlineAddCircleOutline, MdOutlineClose } from "react-icons/md";
import Button from "../../Components/ReuseableComponents/Button";
import InputText from "../../Components/ReuseableComponents/InputText";
import { useState, type FormEvent } from "react";
import Portal from "../../Components/ReuseableComponents/Portal";
import useSizeContext from "../../hooks/useSizeContext";
import ApiLinks from "../../APICalls/ApiLinks";


export default function CreateSize({ handleClose }: { handleClose: () => void }) {

    const { createEntity: createSize } = useSizeContext();

    const [sizeName, setSizeName] = useState('');

    const labelsGroup = `flex flex-col  items-center text-center`;


    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        createSize(ApiLinks.size.create, { Name: sizeName });

        handleClose();
    }


    const createColorPortalChild = <div className="p-4 items-center flex flex-row items-start">
        <div className="">
            <MdOutlineClose onClick={handleClose} className="hover:-translate-y-1 ease-in-out duration-300 transition-transform hover:shadow-lg/30 cursor-pointer text-2xl text-rose-500 bg-white shadow rounded-md" />
        </div>
        <div className="pt-5 text-3xl  flex flex-row gap-2 mx-auto mb-2   justify-center">
            <h2 className="  font-bold text-transparent bg-gradient-to-r from-blue-600 via-rose-500 to-cyan-500 bg-clip-text    border-b border-b-2 pb-3 border-gray-300">
                إضافة مقاس جديد
            </h2>

            <MdAddBox className="" />

        </div>
    </div>

    const actionBar = <form onSubmit={handleSubmit} method="post" className="flex flex-col items-center justify-between gap-8">

        <div className={`${labelsGroup}`}>
            <label htmlFor="sizeName" className="block mb-2   sm:w-1/4">المقاس</label>
            <InputText name="sizeName" onChange={(event) => setSizeName(event.target.value)} value={sizeName} id="sizeName" styles="rounded-md shadow-md/30 border border-2 border-gray-300  " />
        </div>
        <div className="md:justify-self-center md:my-auto">
            <Button className="flex flex-row justify-between  " primary outline hover roundedLg>
                اضف المقاس <MdOutlineAddCircleOutline className="text-xl mr-2" />
            </Button>
        </div>
    </form>

    return <Portal style={`bg-gradient-to-r from-pink-100 to-sky-300 shadow-xl rounded-3xl`} handleClose={handleClose} actionBar={actionBar}>
        {createColorPortalChild}
    </Portal>
}
