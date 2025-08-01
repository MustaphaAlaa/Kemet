import { useState } from "react";
import ApiLinks from "../../../../../APICalls/ApiLinks";
import GetData from "../../../../../APICalls/GetData";
import type { DeliveryCompany } from "../../../../../app/Models/DeliveryCompany";
import { IoSaveSharp } from "react-icons/io5";
import Button from "../../../../../Components/ReuseableComponents/Button";

export default function SelectDeliveryCompany({ orderId, governorateId }: { orderId: number, governorateId: number }) {

    const [selected, setSelected] = useState(-1);

    if (orderId == undefined) {
        return null;
    }

    const { data: deliveryCompanies } = GetData<DeliveryCompany[]>(`${ApiLinks.deliveryCompany.getAllForGovernorate(governorateId)}`);

    console.log('delivery companies dddd')
        console.log(deliveryCompanies);
        console.log('orderid ' + orderId)
        console.log('governorateId '+governorateId);


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
        console.log(selected)
        const dc = deliveryCompanies.find(dc => dc.deliveryCompanyId == value);
        console.log(dc?.name);
        console.log(dc?.deliveryCompanyId);
        // setOrderStatusId(value); 
    }
    return (
        <div className='flex flex-row items-center text-center cursor-pointer   font-bold p-2 rounded-xl'>
            <Button success hover roundedLg onClick={() => console.log("there were characters in a novel.")} >
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
