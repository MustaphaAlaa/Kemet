import { useState, type SetStateAction } from "react";
import type { GovernorateDelivery } from "../../app/Models/GovernorateDelivery";
import { NavLink } from "react-router-dom"; 
import { MdModeEditOutline } from "react-icons/md";
import GovernorateDeliveryEdit from "./GovernorateDeliveryEdit";

 
export  function GovernorateDeliveryCard({ updateModeId, setUpdateModeId, governorateDelivery }: {
    setUpdateModeId: React.Dispatch<SetStateAction<number>>,
    updateModeId: number,
    governorateDelivery: GovernorateDelivery
}) {
    const textColor = `text-teal-900`

    const [updateMode, setUpdateMode] = useState(false);
  
    //  governorateDelivery.deliveryCost = 50
    const [governorateDC, setGovernorateDelivery] = useState(governorateDelivery);
    const id = governorateDC.governorateDeliveryId;

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
                            ${governorateDC.deliveryCost == 0 || governorateDC.deliveryCost == null
            ? ' bg-gradient-to-l from-red-200 to-red-400' :
            ` bg-gradient-to-l from-teal-400 to-teal-200`}
                             p-4 rounded-xl shadow-md/70  `}>
        <NavLink to={""} className={`${textColor} hover:font-bold`} >{governorateDelivery.name}</NavLink>

        <div className="flex flex-row space-x-2 items-center">
            {!(updateMode && updateModeId == id) ?
                <p
                    className={`${governorateDC.deliveryCost == 0 || governorateDC.deliveryCost == null
                        ? 'text-red-950'
                        : textColor}
                       font-bold `}
                >
                    {governorateDC.deliveryCost ?? 0}
                </p>
                : <GovernorateDeliveryEdit setGovernorateDelivery={setGovernorateDelivery} setUpdateMode={setUpdateMode} governorateDelivery={governorateDC}></GovernorateDeliveryEdit>}
            <p>  ج.م  </p>
        </div>

        <span onClick={handleClick}
            className={`${iconStyle} flex-end cursor-pointer rounded-full  p-1 shadow-sm/50 `}>
            <MdModeEditOutline ></MdModeEditOutline>
        </span>
    </div>
}
