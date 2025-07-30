import { useState } from "react";
import ApiLinks from "../../../../../APICalls/ApiLinks";
import GetData from "../../../../../APICalls/GetData";
import type { DeliveryCompany } from "../../../../../app/Models/DeliveryCompany";
import { IoSaveSharp } from "react-icons/io5";
import Button from "../../../../../Components/ReuseableComponents/Button";

export default function SelectDeliveryCompany({ orderId }: { orderId: number }) {

    const [selected, setSelected] = useState(-1);

    if (orderId == undefined) {
        return <div></div>
    }

    const { data: deliveryCompanies } = GetData<DeliveryCompany[]>(`${ApiLinks.deliveryCompany.getAll}`);




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
        // setOrderStatusId(value);

    }
    return (
        <div className='flex flex-row items-center text-center cursor-pointer   font-bold p-2 rounded-xl'>
            <Button success hover roundedLg onClick={() => console.log('meenna meeena is is maa maa ried')} >
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
