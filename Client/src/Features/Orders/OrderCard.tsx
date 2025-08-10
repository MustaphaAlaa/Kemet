import { NavLink } from "react-router-dom";
import type { OrderInfoDTO } from "../../app/Models/OrderInfoDTO";
import OrderStatuses from "../Product/ProductPage/Order/OrderStatus";
import { useState } from "react";
import { LuSaveAll } from "react-icons/lu";
import Button from "../../Components/ReuseableComponents/Button";
import axios from "axios";
import ApiLinks from "../../APICalls/ApiLinks";
import { NavigationLinks } from "../../Navigations/NavigationLinks";
import ShowCodeFromDeliveryCompany from "../Product/ProductPage/Order/CodeFromDeliveryCompany/ShowCodeFromDeliveryCompany";
import { TiEdit } from "react-icons/ti";
import EditCodeFromDeliveryCompany from "../Product/ProductPage/Order/CodeFromDeliveryCompany/EditCodeFromDeliveryCompany";
import OrderReceiptStatuses from "../Product/ProductPage/Order/OrderReceiptStatus";
import type { APIResponse } from "../../app/Models/APIResponse";

function formatDate(isoString: string) {
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


export interface IOrderInfoState {
    quantity: number;
    governorateId: number;
    totalPrice: number;
    governorateDeliveryCost: number | null;
    governorateDeliveryCompanyCost: number | null;
    deliveryCompanyId: number | null;
    orderStatus: number;
}


export interface IOrderReceipt_OrderStatus {
    orderId: number;
    orderStatusId: number;
    orderReceiptStatusId: number | null;
}



export default function OrderCard({ orderInfoDTO, removeOrderFromJson }: { orderInfoDTO: OrderInfoDTO, removeOrderFromJson?: (orderId: number) => void }) {
    const elemStyle = `flex-shrink-0
                       flex flex-row xl:flex-col
                       justify-between
                       p-2 
                       text-gray-500
                       border-l-0
                       xl:border-l-1
                       border-b-1
                       xl:border-b-0
                       border-gray-200
                       px-4
                       `;
    const colStyle = `text-indigo-500 font-semibold  `;

    const [orderReceiptStatus_orderStatus, setOrderReceiptStatus_orderStatus] = useState<IOrderReceipt_OrderStatus>({
        orderId: orderInfoDTO.orderId,
        orderStatusId: orderInfoDTO.orderStatusId,
        orderReceiptStatusId: orderInfoDTO.orderReceiptStatusId

    });

    const handleSaveClicked = async () => {
        const req: IOrderReceipt_OrderStatus = {
            orderId: orderInfoDTO.orderId,
            orderStatusId: orderReceiptStatus_orderStatus?.orderStatusId,
            orderReceiptStatusId: orderReceiptStatus_orderStatus?.orderReceiptStatusId,

        };

        if (orderInfoDTO.orderStatusId != orderReceiptStatus_orderStatus?.orderStatusId) {
            const { data }: { data: APIResponse<IOrderReceipt_OrderStatus> } = await axios.put(`${ApiLinks.orders.updateOrderStatus}`, req);
            if (data.isSuccess) {
                removeOrderFromJson(orderInfoDTO.orderId);
                setOrderReceiptStatus_orderStatus({
                    ...orderReceiptStatus_orderStatus,
                    orderStatusId: data.result?.orderStatusId ?? -1,
                    orderReceiptStatusId: data.result?.orderReceiptStatusId ?? -1
                });
            }
        }

        if (orderInfoDTO.orderReceiptStatusId != orderReceiptStatus_orderStatus.orderReceiptStatusId) {
            const { data }: { data: APIResponse<IOrderReceipt_OrderStatus> } = await axios.put(`${ApiLinks.orders.updateOrderReceiptStatus}`, req);

            if (data.isSuccess)
                setOrderReceiptStatus_orderStatus({ ...orderReceiptStatus_orderStatus })
        }
    }


    const infoState = {
        quantity: orderInfoDTO.quantity,
        governorateId: orderInfoDTO.governorateId,
        totalPrice: orderInfoDTO.totalPrice,
        governorateDeliveryCost: orderInfoDTO.governorateDeliveryCost,
        governorateDeliveryCompanyCost: orderInfoDTO.governorateDeliveryCompanyCost,
        orderStatus: orderInfoDTO.orderStatusId,
        deliveryCompanyId: orderInfoDTO.deliveryCompanyId
    }

    const [updateMode, setUpdateMode] = useState(false);
    const [codeFromDeliveryCompany, setCodeFromDeliveryCompany] = useState(orderInfoDTO.codeFromDeliveryCompany);


    const showOrUpdateCodeFromDeliveryCompany = updateMode ? <EditCodeFromDeliveryCompany
        orderId={orderInfoDTO.orderId}
        codeFromDeliveryCompany={codeFromDeliveryCompany}
        setCodeFromDeliveryCompany={setCodeFromDeliveryCompany}

        updateMode={setUpdateMode}
    ></EditCodeFromDeliveryCompany>
        : <ShowCodeFromDeliveryCompany codeFromDeliveryCompany={codeFromDeliveryCompany}></ShowCodeFromDeliveryCompany>;

    const codeFromDeliveryCompanyJSX = (orderInfoDTO.orderStatusId == 1 || orderInfoDTO.orderStatusId == 2 || orderInfoDTO.orderStatusId == 3)
        ? <div className="flex flex-row xl:flex-col items-center justify-between">
            <TiEdit className="text-green-500 text-2xl mb-1 cursor-pointer" onClick={() => setUpdateMode(!updateMode)} />
            {showOrUpdateCodeFromDeliveryCompany}
        </div>
        : <div>
            <ShowCodeFromDeliveryCompany codeFromDeliveryCompany={codeFromDeliveryCompany} ></ShowCodeFromDeliveryCompany>
        </div>


    return (
        <div className="flex flex-col   md:justify-between bg-white  md:p-2 rounded-xl shadow-md/40 mb-4 p-3  ">
            <div className="flex flex-col md:justify-between items-center mb-4">
                <NavLink className="text-lg font-bold text-indigo-800 underline" to={`${NavigationLinks.orders.orderDetails}/${orderInfoDTO.orderId}`}
                    state={infoState}>تفاصيل الطلب</NavLink>
                <p className="text-sm text-gray-500">رقم الطلب: <span>
                    {orderInfoDTO.orderId}</span></p>
            </div>
            <div className="flex flex-col xl:flex-row xl:justify-between overflow-x-scroll">

                <div className={elemStyle}>
                    <p className={`${colStyle}`}>الاسم</p>
                    <p> {orderInfoDTO.customerName}</p>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>المحافظة</p>
                    <p>{orderInfoDTO.governorateName}</p>
                </div>

                <div className={elemStyle}>
                    <p className={`${colStyle}`}> سعر الشحن للعميل  </p>
                    <p>{orderInfoDTO.governorateDeliveryCost ?? '--'}</p>
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
                    <time dir="ltr">{formatDate(orderInfoDTO.createdAt)}</time>
                </div>
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>حالة الطلب</p>
                    <OrderStatuses
                        setOrderReceiptStatus_orderStatus={setOrderReceiptStatus_orderStatus}
                        orderReceiptStatus_orderStatus={orderReceiptStatus_orderStatus}
                    ></OrderStatuses>
                </div>
                {/* Should be moved to details page */}
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>حالة الاستلام</p>
                    <OrderReceiptStatuses
                        setOrderReceiptStatus_orderStatus={setOrderReceiptStatus_orderStatus}
                        orderReceiptStatus_orderStatus={orderReceiptStatus_orderStatus}
                    ></OrderReceiptStatuses>

                </div>
                {/* Should be appers to be edit in Processing component */}
                <div className={elemStyle}>
                    <p className={`${colStyle}`}>Code Delivery Company</p>
                    {/* <input className="border bg-amber-200 text-gray-800 font-bold"  ></input> */}
                    {codeFromDeliveryCompanyJSX}
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
