import { MdDeleteForever, MdOutlineCreate } from "react-icons/md";
import EditColor from "./EditColor";
import { useState } from "react";
import type { Color } from "../../Models/Color";
import useColorsContext from "../../../hooks/useColorsContext";

export function ColorLabel({ color, setUpdateModeId, updateModeId }: { setUpdateModeId: any, updateModeId: any, color: Color }) {

    const [updateMode, setUpdateMode] = useState(false);
    const { deleteColor } = useColorsContext();



    const border = 'border-b border-gray-300';




    const colorLabelOrForm = {
        "colorLabel": <div className={`m-2 flex flex-row gap-7 items-center ${border}  p-3 bg-gray-200 font-bold`}>
            <span style={{ backgroundColor: `${color.hexacode}` }} className="p-5 rounded-full border border-3 border-white"></span>
            <p>{color.name}</p>
        </div>,
        "form": <EditColor closeUpdateMode={setUpdateMode} color={color}></EditColor>,
    };



    let content = colorLabelOrForm.colorLabel;

    if (updateMode && updateModeId == color.colorId)
        content = colorLabelOrForm.form
    else content = colorLabelOrForm.colorLabel;


    const handleClickUpdateMode = () => {
        setUpdateMode(!updateMode);
        setUpdateModeId(color.colorId);
    }

    const handleClick = () => {
        deleteColor(color.colorId);
    }

    return <div className="m-2 flex flex-col  justify-between shadow-md/10  rounded-xl p-3 bg-gray-200 font-bold">

        {content}
        <div className="m-2 flex flex-row  justify-between text-xl     shadow p-1 rounded-xl bg-gray-100 font-bold">
            <MdDeleteForever onClick={() => handleClick()} className="cursor-pointer bg-red-500  rounded-sm  text-white" />
            <MdOutlineCreate onClick={handleClickUpdateMode} className="cursor-pointer bg-blue-500  rounded-sm  text-white" />
        </div>
    </div>
}








