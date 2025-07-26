import { useState, type FormEvent, type SetStateAction } from "react";
import GetData from "../../APICalls/GetData";
import ApiLinks from "../../APICalls/ApiLinks";
import type { GovernorateDelivery } from "../../app/Models/GovernorateDelivery";
import { NavLink } from "react-router-dom";
import type { APIResponse } from "../../app/Models/APIResponse";
import axios from "axios";
import InputNumber from "../../Components/ReuseableComponents/InputNumber";
import { MdDeliveryDining, MdModeEditOutline } from "react-icons/md";

export function ManageCustomerGovernorateDeliveryList() {

    // const [updateMode, setUpdateMode] = useState(false);

    const [updateModeId, setUpdateModeId] = useState(-1);

    const { data: governorates } = GetData<GovernorateDelivery[]>(`${ApiLinks.governorateDelivery.admin.getAll}`);


    governorates?.sort((a, b) => a.governorateId - b.governorateId);


    const governorateLst = governorates?.map(item => <GovernorateDeliveryCard
        key={item.governorateDeliveryId} governorateDelivery={item}

        updateModeId={updateModeId}
        setUpdateModeId={setUpdateModeId}
    ></GovernorateDeliveryCard>)

    return <div className="flex flex-col space-y-5 bg-gradient-to-l  from-teal-400 to-teal-800  p-5 shadow-xl/50">
        <p className="   bg-white p-4 shadow-xl/30 rounded-xl text-green-900 text-3xl font-extrabold text-center flex flex-row justify-evenly items-center">

            <span className="">  سعر التوصيل للعميل  </span>
            <span className="text-3xl text-blue-800" ><MdDeliveryDining /></span>
        </p>
        <div className="flex flex-col space-y-8 w-11/12  xl:w-1/2 mx-auto">
            {governorateLst}
        </div>
    </div >
}


export function GovernorateDeliveryCard({ updateModeId, setUpdateModeId, governorateDelivery }: {
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



export function GovernorateDeliveryEdit({
    setUpdateMode,
    setGovernorateDelivery,
    governorateDelivery
}: {
    setUpdateMode: React.Dispatch<SetStateAction<boolean>>,
    setGovernorateDelivery: React.Dispatch<SetStateAction<GovernorateDelivery>>,
    governorateDelivery: GovernorateDelivery
}) {


    const [value, setValue] = useState(governorateDelivery.deliveryCost ?? 0);

    const handleSubmit = async (e: FormEvent) => {

        e.preventDefault();
        setUpdateMode(false);
        if (value > 0) {
            const request = {
                GovernorateDeliveryId: governorateDelivery.governorateDeliveryId,
                DeliveryCost: value,
                IsActive: true
            }
            const { data }: { data: APIResponse<GovernorateDelivery> } = await axios.put(
                `${ApiLinks.governorateDelivery.admin.update}`
                , request)

            console.log(data);
            if (data.statusCode == 200) setGovernorateDelivery(data.result!);
        }


    }

    return <form onSubmit={handleSubmit} className=" ">
        <InputNumber defaultStyle={false} value={value > 0 ? value : ''} val={setValue} styles="text-center font-semibold text-blue-800 bg-gray-200 px-2  rounded-xl shadow-sm/50" ></InputNumber>
    </form>
}

