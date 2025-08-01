import { NavLink } from "react-router-dom";
import ApiLinks from "../../../APICalls/ApiLinks";
import GetData from "../../../APICalls/GetData"
import type { DeliveryCompany } from "../../../app/Models/DeliveryCompany";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";


export default function DeliveryCompaniesList() {

    const { data: deliveryCompanies } = GetData<DeliveryCompany[]>(`${ApiLinks.deliveryCompany.getAll}`)


    return (
        <div className="p-5 space-y-5   flex flex-col    items-center">
            {deliveryCompanies?.map(item => <DeliveryCompanyLabel key={item.deliveryCompanyId} deliveryCompany={item}></DeliveryCompanyLabel>)}
        </div>
    )
}


function DeliveryCompanyLabel({ deliveryCompany }: { deliveryCompany: DeliveryCompany }) {
    return <div className="bg-white p-3 w-2/3 rounded-md shadow-md/30 flex flex-row justify-between">
        <NavLink state={{deliveryCompany}} to={`${NavigationLinks.deliveryManagement.deliveryCompany.page}/${deliveryCompany.deliveryCompanyId}`} className={`text-blue-800 `}>{deliveryCompany.name}</NavLink>
        <NavLink to={''} className={`text-blue-800 `}>الطلبات</NavLink>
    </div>
}