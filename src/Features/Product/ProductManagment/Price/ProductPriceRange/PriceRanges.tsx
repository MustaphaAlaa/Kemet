import { useState } from "react";
import { MdOutlineCreate } from "react-icons/md";
import InputNumber from "../../../../../Components/ReuseableComponents/InputNumber";



export function PriceRanges({ setPrice, label, priceValue, editMode = false }: {
    editMode?: boolean,
    label: string;
    priceValue: number | undefined;
    setPrice?: React.Dispatch<React.SetStateAction<number>>;
}) {

    const [updateMode, setUpdateMode] = useState(false);
    const [value, setValue] = useState(priceValue);

    const handleClick = () => {
        setUpdateMode(!updateMode);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        if (setPrice)
            setPrice(value ?? 0);

        setUpdateMode(!updateMode);

        console.log(`price should changed`);
    };



    const content = updateMode ? <form className={`w-full`} onSubmit={handleSubmit}>
        {/* <input type="text" name="" id="" value={value} onChange={handleChange} /> */}
        <InputNumber initValue={priceValue} styles="w-full p-3"  val={setValue}></InputNumber>

    </form> : <p className={` ${!editMode ? 'justify-center w-full' : ''} flex flex-row flex-wrap md:flex-row gap-2 p-3 items-center`}>
        <span className={`font-bold `}>
            {priceValue ? priceValue : 'لا يوجد سعر'}
        </span>

        <span className={`text-gray-800`}>
            {priceValue ? 'ج.م.' : ''}
        </span>
    </p>;


    return <>
        <label className="md:flex-1 shrink-0 text-xl font-bold ">
            {label}
        </label>
        <div className={`md:flex-1 shrink-0 w-full md:w-1/3 flex flex-col md:flex-row items-center justify-between bg-cyan-100 rounded-xl border border-3   overflow-hidden`}>
            {content}
            {editMode &&

                <div className="     text-center p-1 md:p-0 bg-gray-800 self-stretch items-center justify-center flex flex-row">
                    <MdOutlineCreate
                        onClick={handleClick}
                        className="   text-center text-xl cursor-pointer bg-rose-500  rounded-sm  text-white" />
                </div>
            }
        </div>
    </>;
}
