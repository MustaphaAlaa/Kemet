 
import { useState, type FormEvent, type SetStateAction } from "react";
import type { APIResponse } from "../../app/Models/APIResponse";
import type { GovernorateDelivery } from "../../app/Models/GovernorateDelivery";
import InputNumber from "../../Components/ReuseableComponents/InputNumber";
import ApiLinks from "../../APICalls/ApiLinks";
import axios from "axios";

export default function GovernorateDeliveryEdit({
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

