

import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO"
import OrderCard from "../../../Orders/OrderCard"

export default function Orders({ orderInfoDTOs, removeOrderFromJson }: { orderInfoDTOs: OrderInfoDTO[], removeOrderFromJson: (orderId: number) => void }) {
   
  return (
    <div className="text-center bg-gray-100 p-4 rounded shadow-md mb-4 flex flex-col overflow-hidden ">

      {orderInfoDTOs?.length > 0 ? orderInfoDTOs.map(order => (
        <OrderCard key={order.orderId} orderInfoDTO={order} removeOrderFromJson={removeOrderFromJson} />
      )) : 'لا يوجد طلبات لهذة الحالة'}
    </div>
  )
}
