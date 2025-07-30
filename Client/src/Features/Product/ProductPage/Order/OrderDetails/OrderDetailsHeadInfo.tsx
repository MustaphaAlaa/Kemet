

export default function OrderDetailsHeadInfo({ orderId, quantity }: { orderId: number, quantity: number }) {
    return (
        <div className='p-2  bg-indigo-50 flex flex-col space-y-2 border-2 border-gray-400 rounded-xl shadow-xl/30'>
            <div className='flex flex-row  justify-between border-b-2 p-2 border-b-gray-400'>
                <div className='font-bold'>رقم الطلب</div>
                <div>{orderId}</div>
            </div>
            <div className='flex flex-row justify-between space-x-8 border-b-2 p-2 border-b-gray-400'>
                <div className='font-bold'>كود شركة الشحن</div>
                <div>QXH800</div>
            </div>
            <div className='flex flex-row justify-between space-x-8 p-2'>
                <div className='font-bold'>الكمية المطلوبة</div>
                <div>{quantity}</div>
            </div>
        </div>
    )
}
