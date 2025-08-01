import CustomerDetails from './CustomerDetails';
import { useLocation, useParams } from 'react-router-dom';
import OrderItems from './OrderItems/OrderItems';
import OrderDetailsHeadInfo from './OrderDetailsHeadInfo';
import SelectDeliveryCompany from './SelectDeliveryCompany';
import type { IOrderInfoState } from '../OrderCard';

export default function OrderDetailsPage() {
    const params = useParams();
    const orderId = parseInt(params.orderId!);
    const location = useLocation();
    const info: IOrderInfoState = location.state;

    return (
        <div className='p-5 flex flex-col justify-between   space-y-8 '>
            <div className='md:w-1/3 xl:w-1/4 mx-auto'>
                <OrderDetailsHeadInfo orderId={orderId} quantity={info.quantity}></OrderDetailsHeadInfo>
            </div>
            <div>
                <CustomerDetails orderId={orderId}></CustomerDetails>
            </div>
            <div className='rounded-xl overflow-hidden shadow-xl/40 shadow-indigo-800'>
                <OrderItems orderId={orderId}></OrderItems>
            </div>
            <div className='mx-auto bg-indigo-100  rounded-xl overflow-hidden shadow-xl/40 shadow-indigo-800 border-2 border-indigo-400'>

                {/* yet this is the only action in this component so i'll do saving method inside it*/}
                <SelectDeliveryCompany orderId={orderId} governorateId={info.governorateId}></SelectDeliveryCompany>
            </div>
            {/* This card should be have accounting for this order 
                eg.. 
                total, returns, deliver cost, etc.. 
            */}
            <div className='md:w-1/3  p-3  md:mx-auto flex flex-col items-center  bg-gradient-to-l from-white to-gray-100 border-3 border-cyan-900 rounded-xl'>
                <div className='md:w-2/3 w-[80%] p-3  flex flex-row justify-between items-center'>
                    <div className='text-[1.5rem] text-blue-700 font-bold'>إجمالى سعر الطلب</div>
                    <div className='text-[1.2rem]  font-semibold flex flex-row justify-between items-center space-x-2'>
                        <p>{info?.totalPrice}</p>
                        <p>ج.م</p>
                    </div>
                </div>
                <div className='md:w-2/3 w-[80%] p-3  flex flex-row justify-between items-center'>
                    <div className='text-[1.5rem] text-blue-700 font-bold'> سعر توصيل شركة الشحن</div>
                    <div className='text-[1.2rem]  font-semibold flex flex-row justify-between items-center space-x-2'>
                        <p>

                            {/* should price for governorate delivery appear here */}
                            {info.governorateDeliveryCost}
                        </p>
                        <p>ج.م</p>
                    </div>
                </div>
            </div>
        </div>
    )
}
