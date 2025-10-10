import ApiLinks from "../../../../../APICalls/ApiLinks";
import GetData from "../../../../../APICalls/GetData";
import type { DeliveryCompany } from "../../../../../app/Models/DeliveryCompany";
import { IoSaveSharp } from "react-icons/io5";
import Button from "../../../../../Components/ReuseableComponents/Button";
import axios from "axios";
import type { APIResponse } from "../../../../../app/Models/APIResponse";
import type { DeliveryCompanyDetailsDTO } from "../../../../../app/Models/DeliveryCompanyDetailsDTO";
import { useState } from "react";
 

export default function SelectDeliveryCompany({ orderId, governorateId,
    setGovernorateDeliveryCompanyCost,
    deliveryCompanyId
}: {
    orderId: number,
    governorateId: number,
    deliveryCompanyId: number | null,
    setGovernorateDeliveryCompanyCost: React.Dispatch<React.SetStateAction<number | null>>

}) {

    
    if (orderId == undefined) {
        return null;
    }
    
    const { data: deliveryCompanies } = GetData<DeliveryCompany[]>(ApiLinks.deliveryCompany.getAllForGovernorate(governorateId));
    const [selected, setSelected] = useState(deliveryCompanyId ?? -1);




 

    const statues = deliveryCompanies?.map(item => (
        <option
            key={item.deliveryCompanyId}
            value={item.deliveryCompanyId}
            className=''
        >
            {item.name}
        </option>
    ));

    const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const value = parseInt(event?.target.value)
        setSelected(value)
        
    }


    const onSaveButtonClicked = async () => {
        const { data }: { data: APIResponse<DeliveryCompanyDetailsDTO> } = await axios.put(ApiLinks.orders.updateOrderDeliveryCompany(orderId, selected, governorateId));
        setGovernorateDeliveryCompanyCost(data.result?.governorateDeliveryCompanyCost ?? 0);
        setSelected(data.result?.deliveryCompanyId ?? -1);
    }
    
    return (
        <div className='flex flex-row items-center text-center cursor-pointer   font-bold p-2 rounded-xl'>
            <Button success hover roundedLg onClick={onSaveButtonClicked} >
                <IoSaveSharp className="text-white text-3xl" />
            </Button>
            <select name='orderStatuses' autoFocus className='text-center cursor-pointer bg-white font-bold p-2 rounded-xl'
                value={selected} onChange={handleChange} aria-label="Select delivery company" title="Select delivery company">
                <option value={-1}>-- اختار شركة الشحن --</option>
                {statues}
            </select>

        </div>


    )
}
