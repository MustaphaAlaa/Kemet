import type { FormEvent } from "react";
import ApiLinks from "../../../../APICalls/ApiLinks";
import GetData from "../../../../APICalls/GetData";
import type { OrderStatusReadDTO } from "../../../../app/Models/OrderStatus";
import type { IOrderReceipt_OrderStatus } from "../../../Orders/OrderCard";



export default function OrderStatuses(
    { orderReceiptStatus_orderStatus, setOrderReceiptStatus_orderStatus }
        : {
            orderReceiptStatus_orderStatus: IOrderReceipt_OrderStatus,
            setOrderReceiptStatus_orderStatus: React.Dispatch<React.SetStateAction<IOrderReceipt_OrderStatus>>
        }) {
    if (orderReceiptStatus_orderStatus.orderId == undefined) {
        return <div></div>
    }

    const { data } = GetData<OrderStatusReadDTO[]>(ApiLinks.orders.helper.orderStatuses)



    const statues = data?.map(item => (
        <option
            key={item.orderStatusId}
            value={item.orderStatusId}
            className=''
        >
            {item.name}
        </option>
    ));

    const handleChange = (event: FormEvent) => {
        const value = parseInt(event?.target.value)
        setOrderReceiptStatus_orderStatus(prevState => ({ ...prevState, orderStatusId: value }));
    }
    return (

        <select name='orderStatuses' autoFocus className='flex flex-row items-center text-center cursor-pointer bg-cyan-50 font-bold p-2 rounded-xl'
            value={orderReceiptStatus_orderStatus.orderStatusId} onClick={() => console.log('ou!')} onChange={handleChange}>
            {statues}

        </select>

    )
} 