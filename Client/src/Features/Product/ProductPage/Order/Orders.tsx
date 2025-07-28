 

import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO"
import OrderCard from "./OrderCard"

export default function Orders({orderInfoDTOs}: { orderInfoDTOs: OrderInfoDTO[] }) {
  console.log(orderInfoDTOs)
  return (
    <div className="text-center bg-gray-100 p-4 rounded shadow-md mb-4 flex flex-col overflow-hidden ">
      <h2 className="text-lg font-bold mb-2">الطلبات</h2>
      
      {orderInfoDTOs.map(order => (
        <OrderCard key={order.orderId} orderInfoDTO={order} />
      ))}
    </div>
  )
}
