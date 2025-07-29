import React from 'react'
import CustomerDetails from './CustomerDetails';
import { useParams } from 'react-router-dom';
import OrderItems from './OrderItems/OrderItems';

export default function OrderDetailsPage() {
    const params = useParams();
    const orderId = params.orderId;


    return (
        <div className='p-5 flex flex-col justify-between   space-y-8 '>
            <div>
                <CustomerDetails orderId={parseInt(orderId!)}></CustomerDetails>
            </div>
            <div>

            <OrderItems orderId={parseInt(orderId!)}></OrderItems>
            </div>
        </div>
    )
}
