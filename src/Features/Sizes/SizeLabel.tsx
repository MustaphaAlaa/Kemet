import { MdDeleteForever, MdOutlineCreate } from "react-icons/md";

import { useState } from "react"; 
import type { Size } from "../../app/Models/Size";
import EditSize from "./EditSize";
import useSizeContext from "../../hooks/useSizeContext";
import ApiDomain from "../../app/Models/ApiDomain";
import { CardLabel } from "../../Components/ReuseableComponents/CardLabel"; 
import { SizeCircle } from "./SizeCircle";






export function SizeLabel({ size, setUpdateModeId, updateModeId }: { setUpdateModeId: any, updateModeId: any, size: Size }) {

    const [updateMode, setUpdateMode] = useState(false);
    const { deleteEntity: deleteSize } = useSizeContext();


 




    const colorLabelOrForm = {
        // "colorLabel": <p className="p-3 text-xl rounded-full border border-3 border-white text-center bg-sky-100">
        //      {size.name} 
        // </p>
        // ,
         "colorLabel":  <SizeCircle size={size}></SizeCircle>
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
        deleteSize(`${ApiDomain}/api/a/size`, {data:{ SizeId: size.sizeId }});
    }

    return <CardLabel handleDelete={handleClick} handleUpdateMode={handleClickUpdateMode}>
        {content}
    </CardLabel>
}








