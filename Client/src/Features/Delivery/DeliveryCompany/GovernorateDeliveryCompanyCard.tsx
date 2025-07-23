import { useState, type SetStateAction } from "react";
import type { GovernorateDeliveryCompany } from "../../../app/Models/GovernorateDeliveryCompany";
import { NavLink } from "react-router-dom";
import { GovernorateDeliveryCompanyEdit } from "./GovernorateDeliveryCompanyEdit";
import { MdModeEditOutline } from "react-icons/md";

 

export function GovernorateDeliveryCompanyCard({ updateModeId, setUpdateModeId, governorateDeliveryCompany }: {
    setUpdateModeId: React.Dispatch<SetStateAction<number>>,
    updateModeId: number,
    governorateDeliveryCompany: GovernorateDeliveryCompany
}) {
    const textColor = `text-cyan-800`

    const [updateMode, setUpdateMode] = useState(false);

    const [governorateDC, setGovernorateDeliveryCompany] = useState(governorateDeliveryCompany);
    const id = governorateDC.governorateDeliveryCompanyId;

    const handleClick = () => {
        if (updateMode && updateModeId == id) {
            setUpdateMode(false);
            setUpdateModeId(-1);
        } if (updateMode && updateModeId != id) {
            setUpdateModeId(id);

        } else {
            setUpdateMode(!updateMode);
            setUpdateModeId(id);
        }
    }

    const iconStyle = (updateMode && updateModeId == id) ? ` text-green-800 bg-green-100 shadow-green-900`
        : `text-cyan-700 bg-cyan-100 shadow-cyan-900`
        ;
    return <div className={`flex flex-row items-center justify-between
                            ${governorateDC.deliveryCost == 0 || governorateDC.deliveryCost == null? ' bg-gradient-to-l from-red-50 to-red-100' : ` bg-gradient-to-l from-gray-50 to-sky-100`}
                             p-4 rounded-xl shadow-md/50 shadow-cyan-700`}>
        <NavLink to={""} className={`${textColor} hover:font-bold`} >{governorateDeliveryCompany.name}</NavLink>

        <div className="flex flex-row space-x-2 items-center">
            {!(updateMode && updateModeId == id) ?
                <p className={`${governorateDC.deliveryCost == 0 || governorateDC.deliveryCost == null? 'text-red-500' : textColor} font-bold `} >{governorateDC.deliveryCost ?? 0} </p>
                : <GovernorateDeliveryCompanyEdit setGovernorateDeliveryCompany={setGovernorateDeliveryCompany} setUpdateMode={setUpdateMode} governorateDeliveryCompany={governorateDC}></GovernorateDeliveryCompanyEdit>}
            <p>  ج.م  </p>
        </div>

        <span onClick={handleClick}
            className={`${iconStyle} flex-end cursor-pointer rounded-full  p-1 shadow-sm/50 `}>
            <MdModeEditOutline ></MdModeEditOutline>
        </span>
    </div>
}