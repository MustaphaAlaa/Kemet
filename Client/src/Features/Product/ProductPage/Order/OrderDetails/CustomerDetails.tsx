import GetData from '../../../../../APICalls/GetData'; 
import ApiLinks from '../../../../../APICalls/ApiLinks'; 
import type { GetCustomerOrdersInfo } from '../../../../../app/Models/GetCustomerOrdersInfo';

// export default function CustomerDetails({ orderId }: { orderId: number }) {
export default function CustomerDetails({orderId}:{orderId:number}) {

    const { data } = GetData<GetCustomerOrdersInfo>(`${ApiLinks.orders.customerInfo(orderId)}`);
    console.log(data);
    const elemStyle = " flex flex-row md:flex-col justify-between p-2 text-gray-500";
    const colStyle = `text-indigo-500  `;

    const child = 'w-full flex flex-row justify-between   items-center flex-shrink-0 border-b-1 border-gray-200 p-1 m-3';
    const lastChild = 'w-full flex flex-row justify-between  items-center flex-shrink-0 p-1 m-3'; // no border
    return (
        <div className='flex flex-col justify-between items-center bg-white md:w-3/4 xl:w-1/3 mx-auto p-4 shadow-xl/30 rounded-xl border-4 border-cyan-800 text-center'>
            <h2 className='text-blue-800 text-xl font-bold'>بيانات العميل</h2>
            <div className={child}>
                <div className={colStyle}>الاسم</div>
                <div className={elemStyle}>{data?.firstName + ' ' + data?.lastName}</div>
            </div>
            <div className={child}>
                <div className={colStyle}>رقم الموبايل</div>
                <div className={elemStyle}>{data?.phoneNumber}</div>
            </div>
            <div className={child}>
                <div className={colStyle}>المحافظة</div>
                <div className={elemStyle}>{data?.governorateName}</div>
            </div>
            <div className={lastChild}>
                <div className={colStyle}>العنوان</div>
                <div className={elemStyle}>{data?.streetAddress}</div>
            </div>
        </div>
    )
}
