import { useState, type SetStateAction } from "react";
import ApiLinks from "../../../APICalls/ApiLinks";
import type { APIResponse } from "../../../app/Models/APIResponse";
import type { GovernorateDeliveryCompany } from "../../../app/Models/GovernorateDeliveryCompany";
import InputNumber from "../../../Components/ReuseableComponents/InputNumber";
import axios from "axios";

export function GovernorateDeliveryCompanyEdit({
    setUpdateMode,
    setGovernorateDeliveryCompany,
    governorateDeliveryCompany
}: {
    setUpdateMode: React.Dispatch<SetStateAction<boolean>>,
    setGovernorateDeliveryCompany: React.Dispatch<SetStateAction<GovernorateDeliveryCompany>>,
    governorateDeliveryCompany: GovernorateDeliveryCompany
}) {


    const [value, setValue] = useState(governorateDeliveryCompany.deliveryCost ?? 0);

    const handleSubmit = async (e) => {

        e.preventDefault();
        setUpdateMode(false);
        if (value > 0) {
            const request = {
                GovernorateDeliveryCompanyId: governorateDeliveryCompany.governorateDeliveryCompanyId,
                DeliveryCost: value,
                IsActive: true
            }
            const { data }: { data: APIResponse<GovernorateDeliveryCompany> } = await axios.put(
                `${ApiLinks.deliveryCompany.updateGovernorateCost(governorateDeliveryCompany.deliveryCompanyId)}`
                , request)

            console.log(data);
            if (data.statusCode == 200) setGovernorateDeliveryCompany(data.result!);
        }


    }

    return <form onSubmit={handleSubmit} className=" ">
        <InputNumber defaultStyle={false} value={value > 0 ? value : ''} val={setValue} styles="text-center font-semibold text-blue-800 bg-gray-200 px-2  rounded-xl shadow-sm/50" ></InputNumber>
    </form>
}