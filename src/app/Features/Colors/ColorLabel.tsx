import { MdDeleteForever, MdOutlineCreate } from "react-icons/md";
import EditColor from "./EditColor";
import { useState } from "react";
import type { Color } from "../../Models/Color";

export function ColorLabel({ color  }: { color: Color }) {

    const [updateMode, setUpdateMode] = useState(false);
    // const [isColorUpdated, setColorIsUpdated] = useState<boolean>(false);

    const border = 'border-b border-gray-300';


    // let content = <p>{color.name}</p>;
    let content = <div className={`m-2 flex flex-row gap-7 items-center ${border}  p-3 bg-gray-200 font-bold`}>
        <span style={{ backgroundColor: `${color.hexacode}` }} className="p-5 rounded-full border border-3 border-white"></span>
        <p>{color.name}</p>
    </div>

    // if (isColorUpdated) {
    //     setUpdateMode(false);
    // }
    if (updateMode)
        content = <EditColor closeUpdateMode={setUpdateMode} color={color}></EditColor>


    return <div className="m-2 flex flex-col  justify-between shadow-md/10  rounded-xl p-3 bg-gray-200 font-bold">

        {content}

        <div className="m-2 flex flex-row  justify-between text-xl     shadow p-1 rounded-xl bg-gray-100 font-bold">
            <MdDeleteForever className="cursor-pointer bg-red-500  rounded-sm  text-white" />
            <MdOutlineCreate onClick={() => setUpdateMode(!updateMode)} className="cursor-pointer bg-blue-500  rounded-sm  text-white" />
        </div>
    </div>
}








