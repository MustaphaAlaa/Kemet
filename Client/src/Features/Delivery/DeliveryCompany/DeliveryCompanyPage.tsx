import { NavLink, useLocation } from "react-router-dom"
import type { DeliveryCompany } from "../../../app/Models/DeliveryCompany"; 
import GetData from "../../../APICalls/GetData";
import ApiLinks from "../../../APICalls/ApiLinks";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";
import type { GovernorateDeliveryCompany } from "../../../app/Models/GovernorateDeliveryCompany";
import { useState } from "react";
import { DeliveryCompanyInfo } from "./DeliveryCompanyInfo";
import { GovernorateDeliveryCompanyCard } from "./GovernorateDeliveryCompanyCard";

export default function DeliveryCompanyPage() {
    const location = useLocation();

    const deliveryCompany: DeliveryCompany = location.state.deliveryCompany;



    const navLinkStyle = `text-indigo-950 shadow-md/20 
                          border-3 border-white 
                          bg-gradient-to-l via-pink-200 from-pink-100 to-pink-300
                          p-1 px-3 rounded-md font-bold`
    return (
        <div className="  space-y-20  ">
            <DeliveryCompanyInfo deliveryCompany={deliveryCompany}></DeliveryCompanyInfo>
            <div className="flex flex-row flex-wrap justify-evenly w-full">

                <NavLink to={`${NavigationLinks.deliveryManagement.deliveryCompany.governorates}/${deliveryCompany.deliveryCompanyId}`} state={{ deliveryCompany }} className={`${navLinkStyle}`}>المحافظات</NavLink>
                <NavLink to="" className={`${navLinkStyle}`}>الطلبات</NavLink>
                <NavLink to="" className={`${navLinkStyle}`}>المرتجعات</NavLink>
                <NavLink to={''} className={` ${navLinkStyle}`}>الحساب</NavLink>

            </div>

        </div>
    )
}



/**
 * __________ Dc
 * ______ Governorate
 * ____ Cost
 * ____ Orders
 * ______ Orders for all governorate
 */


export function DeliveryCompanyGovernorateList() {
    const location = useLocation();
    const deliveryCompany: DeliveryCompany = location.state.deliveryCompany;

    // const [updateMode, setUpdateMode] = useState(false);
    const [updateModeId, setUpdateModeId] = useState(-1);

    const { data: governorates } = GetData<GovernorateDeliveryCompany[]>(`${ApiLinks.deliveryCompany.activeGovernorates(deliveryCompany.deliveryCompanyId)}`);


    governorates?.sort((a, b) => a.governorateId - b.governorateId);


    const governorateLst = governorates?.map(item => <GovernorateDeliveryCompanyCard
        key={item.governorateDeliveryCompanyId} governorateDeliveryCompany={item}

        updateModeId={updateModeId}
        setUpdateModeId={setUpdateModeId}
    ></GovernorateDeliveryCompanyCard>)

    return <div className="flex flex-col space-y-5">
        <DeliveryCompanyInfo deliveryCompany={deliveryCompany}></DeliveryCompanyInfo>
        <div className="flex flex-col space-y-8 w-11/12  xl:w-1/2 mx-auto">
            {governorateLst}
        </div>
    </div >
}
 

 
 
export function DeliveryCompanyGovernorateOrders() {
    return <div></div >
}

export function DeliveryCompanyGovernorateCost() {
    return <div></div >
} 
