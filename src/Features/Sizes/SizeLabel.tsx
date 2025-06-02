import { MdDeleteForever, MdOutlineCreate } from "react-icons/md";

import { useState } from "react";
import useColorsContext from "../../hooks/useColorsContext";
import type { Size } from "../../app/Models/Size";
import EditSize from "./EditSize";
import useSizeContext from "../../hooks/useSizeContext";
import domain from "../../app/Models/domain";



type cardLabelArguments = { [x: string]: any, handleDelete: () => void, handleUpdateMode: () => void }
export function CardLabel({ children, handleDelete, handleUpdateMode }: cardLabelArguments) {
    return <div className="m-2 flex flex-col  justify-between shadow-md/10  rounded-xl p-3 bg-gray-100 font-bold text-indigo-800">

        {children}
        <div className="m-2 flex flex-row  justify-between text-xl     shadow p-1 rounded-xl bg-white font-bold">
            <MdDeleteForever onClick={handleDelete} className="cursor-pointer bg-red-500  rounded-sm  text-white" />
            <MdOutlineCreate onClick={handleUpdateMode} className="cursor-pointer bg-blue-500  rounded-sm  text-white" />
        </div>
    </div>
}




export function SizeLabel({ size, setUpdateModeId, updateModeId }: { setUpdateModeId: any, updateModeId: any, size: Size }) {

    const [updateMode, setUpdateMode] = useState(false);
    const { deleteEntity: deleteSize } = useSizeContext();


 




    const colorLabelOrForm = {
        "colorLabel": <p className="p-3 text-xl rounded-full border border-3 border-white text-center bg-sky-100">
             {size.name} 
        </p>
        ,
        "form": <EditSize closeUpdateMode={setUpdateMode} size={size}></EditSize>,
    };



    let content = colorLabelOrForm.colorLabel;

    if (updateMode && updateModeId == size.sizeId)
        content = colorLabelOrForm.form
    else content = colorLabelOrForm.colorLabel;


    const handleClickUpdateMode = () => {
        setUpdateMode(!updateMode);
        setUpdateModeId(size.sizeId);
    }

    const handleClick = () => {
        deleteSize(`${domain}/api/a/size`, {data:{ SizeId: size.sizeId }});
    }

    return <CardLabel handleDelete={handleClick} handleUpdateMode={handleClickUpdateMode}>
        {content}
    </CardLabel>
}








