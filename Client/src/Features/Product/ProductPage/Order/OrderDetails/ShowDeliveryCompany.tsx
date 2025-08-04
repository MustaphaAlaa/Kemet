import React, { useState } from 'react'
import type { DeliveryCompany } from '../../../../../app/Models/DeliveryCompany';
import GetData from '../../../../../APICalls/GetData';
import ApiLinks from '../../../../../APICalls/ApiLinks';

export default function ShowDeliveryCompany({ deliveryCompanyId }: { deliveryCompanyId: number | null }) {
    if (deliveryCompanyId == null)
        return null


    const { data: deliveryCompany } = GetData<DeliveryCompany>(`${ApiLinks.deliveryCompany.get(deliveryCompanyId)}`);
    console.log("lmbdgkgm gmfs gmrso pmgrwop mgropwm owp");
    console.log("lmbdgkgm gmfs gmrso pmgrwop mgropwm owp");
    console.log("lmbdgkgm gmfs gmrso pmgrwop mgropwm owp");
    console.log(deliveryCompanyId)
    console.log("lmbdgkgm gmfs gmrso pmgrwop mgropwm owp");
    console.log("lmbdgkgm gmfs gmrso pmgrwop mgropwm owp");
    console.log(deliveryCompany);
    return (
        <div className='flex flex-col items-center text-center space-y-2'>
            <h3 className='font-semibold'>شركة الشحن</h3>
            <div className='bg-gray-100 font-bold text-xl px-5 py-2 w-full'>

                {deliveryCompany?.name}
            </div>
        </div>
    )
}
