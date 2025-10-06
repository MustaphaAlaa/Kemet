import type { FormEvent } from 'react';
import ApiLinks from '../../../../APICalls/ApiLinks';
import GetData from '../../../../APICalls/GetData';
import type { OrderReceiptStatusReadDTO } from '../../../../app/Models/OrderReceiptStatusReadDTO';
import type { IOrderReceipt_OrderStatus } from '../../../Orders/OrderCard';

export default function OrderReceiptStatuses(
    { orderReceiptStatus_orderStatus, setOrderReceiptStatus_orderStatus }
        : {
            orderReceiptStatus_orderStatus: IOrderReceipt_OrderStatus,
            setOrderReceiptStatus_orderStatus: React.Dispatch<React.SetStateAction<IOrderReceipt_OrderStatus>>
        }
) {
    if (orderReceiptStatus_orderStatus.orderId == undefined) {
        return <div></div>
    }

    const { data } = GetData<OrderReceiptStatusReadDTO[]>(ApiLinks.orders.helper.orderReceiptStatuses)



    const statues = data?.map(item => (
        <option
            key={item.orderReceiptStatusId}
            value={item.orderReceiptStatusId}
            className=''
        >
            {item.name}
        </option>
    ));

    const handleChange = (event: FormEvent) => {
        const value = parseInt(event?.target.value)

        setOrderReceiptStatus_orderStatus({
            ...orderReceiptStatus_orderStatus,
            orderReceiptStatusId: value,
        });

    }
    return (

        <select name='orderStatuses' autoFocus className='flex flex-row items-center text-center cursor-pointer bg-cyan-50 font-bold p-2 rounded-xl'
            value={orderReceiptStatus_orderStatus.orderReceiptStatusId ?? -1} onClick={() => console.log('ou!')} onChange={handleChange}>

            <option
                value={-1}
            >
                -- أختار حالة استلام الطلب --
            </option>
            {statues}

        </select>

    )
} 