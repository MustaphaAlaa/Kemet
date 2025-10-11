import { useState } from "react";
import GetData from "../../APICalls/GetData";
import type { GovernorateDelivery } from "../../app/Models/GovernorateDelivery";
import { GovernorateDeliveryCard } from "./GovernorateDeliveryCard";
import { MdDeliveryDining } from "react-icons/md";
import ApiLinks from "../../APICalls/ApiLinks";



export function ManageCustomerGovernorateDeliveryList() {


    const [updateModeId, setUpdateModeId] = useState(-1);

    const { data: governorates } = GetData<GovernorateDelivery[]>(ApiLinks.governorateDelivery.admin.getAll);


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



