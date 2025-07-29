import React from 'react'
import CustomerDetails from './CustomerDetails';
import { useParams } from 'react-router-dom';

export default function OrderDetailsPage() {
    const params = useParams();
    const orderId = params.orderId;


    return (
        <div className='p-5'>
            <CustomerDetails orderId={parseInt(orderId)}></CustomerDetails>
        </div>
    )
}
