import { NavLink } from "react-router-dom";
import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO";
import OrderStatuses from "./OrderStatus";
import { useState } from "react";
import { LuSaveAll } from "react-icons/lu";
import Button from "../../../../Components/ReuseableComponents/Button";
import axios from "axios";
import ApiLinks from "../../../../APICalls/ApiLinks";
import { NavigationLinks } from "../../../../Navigations/NavigationLinks";
import OrderDetailsPage from "./OrderDetails/OrderDetailsPage";

function formatDate(isoString) {
    const date = new Date(isoString);

    const year = date.getFullYear();
    const month = date.getMonth() + 1; // Months are 0-based
    const day = date.getDate();

    let hours = date.getHours();
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const ampm = hours >= 12 ? 'PM' : 'AM';

    // Convert to 12-hour format
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'

    return `${year}-${month}-${day} ${hours}:${minutes} ${ampm}`;
}






export default function OrderCard({ orderInfoDTO, removeOrderFromJson }: { orderInfoDTO: OrderInfoDTO, removeOrderFromJson: (orderId: number) => void }) {
    const elemStyle = "flex-shrink-0 border-l-0 md:border-l-1 border-b-1 md:border-b-0 border-gray-200 px-4 flex flex-row md:flex-col justify-between p-2 text-gray-500";
    const colStyle = `text-indigo-500 font-semibold`;

    const [newOrderStatusId, setOrderStatusId] = useState(orderInfoDTO.orderStatusId);
    const [newOrderReceiptStatusId, setOrderReceiptStatusId] = useState(orderInfoDTO.orderReceiptStatusId);



    const handleSaveClicked = async () => {
        // console.log(`old osid`, orderInfoDTO.orderStatusId);
        // console.log(`new osid`, newOrderStatusId);
        if (orderInfoDTO.orderStatusId != newOrderStatusId) {
            const { data } = await axios.put(`${ApiLinks.orders.updateOrderStatus(orderInfoDTO.orderId, newOrderStatusId)}`);
            if (data.isSuccess)
                removeOrderFromJson(orderInfoDTO.orderId);
        }

        if (orderInfoDTO.orderReceiptStatusId != null && orderInfoDTO.orderReceiptStatusId != newOrderReceiptStatusId) {
            const { data } = await axios.put(`${ApiLinks.orders.updateOrderReceiptStatus(orderInfoDTO.orderId, newOrderReceiptStatusId!)}`);
            // check if it updated then remove it from json

        }
    }

    return (
        <div className="flex flex-col   md:justify-between bg-white  md:p-2 rounded-xl shadow-md/40 mb-4 p-3  ">
            <div className="flex flex-col md:justify-between items-center mb-4">
                <NavLink className="text-lg font-bold text-indigo-800 underline" to={`${NavigationLinks.orders.orderDetails}/${orderInfoDTO.orderId}`}>تفاصيل الطلب</NavLink>
                <p className="text-sm text-gray-500">رقم الطلب: <span>
                    {orderInfoDTO.orderId}</span></p>
            </div>
            <div className="flex flex-col md:flex-row md:justify-between         overflow-x-scroll">

                <div className={elemStyle}>
                    <p className={`${colStyle}`}>الاسم</p>
                    <p> {orderInfoDTO.customerName}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>المحافظة</p>
                    <p>{orderInfoDTO.governorateName}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>عنوان الشارع</p>
                    <p>{orderInfoDTO.streetAddress}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>إجمالي السعر</p>
                    <p>
                        <span className="text-gray-500"> ج.م    </span>
                        {orderInfoDTO.totalPrice}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>الكمية</p>
                    <p>{orderInfoDTO.quantity}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>تاريخ الطلب</p>
                    <p dir="ltr">{formatDate(orderInfoDTO.createdAt)}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>حالة الطلب</p>
                    <OrderStatuses orderId={orderInfoDTO.orderId} orderStatusId={orderInfoDTO.orderStatusId}
                        setOrderStatusId={setOrderStatusId}></OrderStatuses>
                </div>
                {/* Should be moved to details page */}
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>حالة الاستلام</p>
                    <p> Should be drop menu</p>
                                        <OrderStatuses orderId={orderInfoDTO.orderId} orderStatusId={orderInfoDTO.orderStatusId}
                        setOrderStatusId={setOrderStatusId}></OrderStatuses>

                </div>
                {/* Should be appers to be edit in Processing component */}
                <div className={elemStyle}> 
                    <p className={`${colStyle}`}>Code Delivery Company</p>
                    <input className="border bg-amber-200 text-gray-800 font-bold"  ></input>
                </div>
            </div >
            <div className="self-center">
                <Button success roundedMd hover onClick={handleSaveClicked} >
                    <LuSaveAll className="text-2xl" />
                </Button>
            </div>

        </div>

    )
}
