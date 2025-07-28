
import { useState } from 'react';
import ApiLinks from '../../../../APICalls/ApiLinks';
import GetData from '../../../../APICalls/GetData';
import type { OrderStatusReadDTO } from '../../../../app/Models/OrderStatus';

export default function OrderStatuses({ orderId, orderStatusId, setOrderStatusId }: { orderId: number, orderStatusId: number, setOrderStatusId: React.Dispatch<React.SetStateAction<number>> }) {
    if (orderId == undefined) {
        return <div></div>
    }

    const { data } = GetData<OrderStatusReadDTO[]>(`${ApiLinks.orders.orderStatuses}`)

    const [selected, setSelected] = useState(orderStatusId);


    const statues = data?.map(item => (
        <option
            key={item.orderStatusId}
            value={item.orderStatusId}
            className=''
        >
            {item.name}
        </option>
    ));

    const handleChange = (event) => {
        const value  = parseInt(event?.target.value)
        setSelected(value)
        setOrderStatusId(value);

    }
    return (

        <select name='orderStatuses' autoFocus className='flex flex-row items-center text-center cursor-pointer bg-cyan-50 font-bold p-2 rounded-xl'
            value={selected} onClick={() => console.log('ou!')} onChange={handleChange}>
            {statues}

        </select>

    )
} 