import { useState, type FormEvent } from "react";
import InputText from "../../Components/ReuseableComponents/InputText";
import Button from "../../Components/ReuseableComponents/Button";
import { MdSave } from "react-icons/md";
import type { Size } from "../../app/Models/Size";
import useSizeContext from "../../hooks/useSizeContext";
import ApiDomain from "../../app/Models/ApiDomain";

export default function EditSize({ closeUpdateMode, size }: { closeUpdateMode: any, size: Size }) {

    const { updateEntity: updateSize } = useSizeContext();

    const [sizeName, setSizeName] = useState(size.name);

    const labelsGroup = `flex flex-col md:flex-row  items-center text-center`;


    const handleSubmit = (event: FormEvent) => {
        event.preventDefault();

        updateSize(`${ApiDomain}/api/a/size`, { sizeId: size.sizeId, Name: sizeName });
        closeUpdateMode(false);

    }


    return (

        <form onSubmit={handleSubmit} method="put" className=" flex flex-col items-center">

            <div className={`${labelsGroup}`}>
                <label htmlFor="sizeName" className="block mb-2   sm:w-1/4">المقاس</label>
                <InputText name="sizeName" onChange={(event) => setSizeName(event.target.value)} value={sizeName} id="sizeName" styles="text-center  rounded-md shadow-md/30 border border-2 border-gray-300  " />
            </div>


            <div className="md:justify-self-center md:my-auto">
                <Button primary outline hover roundedLg>
                    حفظ المقاس <MdSave className="text-xl ml-1" />
                </Button>
            </div>
        </form>
    );
}
