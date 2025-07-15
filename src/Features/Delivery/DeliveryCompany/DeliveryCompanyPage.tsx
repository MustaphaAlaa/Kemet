import { NavLink, useLocation } from "react-router-dom"
import type { DeliveryCompany } from "../../../app/Models/DeliveryCompany";
import { MdCheck, MdClose, MdModeEditOutline } from "react-icons/md";
import GetData from "../../../APICalls/GetData";
import ApiLinks from "../../../APICalls/ApiLinks";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";
import type { GovernorateDeliveryCompany } from "../../../app/Models/GovernorateDeliveryCompany";
import { useState, type SetStateAction } from "react";
import InputNumber from "../../../Components/ReuseableComponents/InputNumber";
import axios from "axios";
import type { APIResponse } from "../../../app/Models/APIResponse";

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


//!!!!!! AI AIA AI AI AI AI AI IA AIA  AI
// This component no longer needs its own state!
export function GovernorateDeliveryCompanyCardAI({ governorateDeliveryCompany, isEditing, onEditClick }) {
    const textColor = `text-cyan-800`;
    const { deliveryCost, name } = governorateDeliveryCompany;

    return (
        <div className={`...`}>
            <p>{name}</p>
            <div>
                {isEditing ? (
                    // When edit is done, it calls a function from the parent.
                    <GovernorateDeliveryCompanyEdit
                        governorateDeliveryCompany={governorateDeliveryCompany}
                        onUpdateComplete={() => onEditClick(-1)} // Tell parent to close edit mode
                    />
                ) : (
                    <p>{deliveryCost ?? 0} ج.م</p>
                )}
            </div>
            <span onClick={() => onEditClick(governorateDeliveryCompany.governorateDeliveryCompanyId)}>
                <MdModeEditOutline />
            </span>
        </div>
    );
}

export function GovernorateDeliveryCompanyCard({ updateModeId, setUpdateModeId, governorateDeliveryCompany }: {
    setUpdateModeId: React.Dispatch<SetStateAction<number>>,
    updateModeId: number,
    governorateDeliveryCompany: GovernorateDeliveryCompany
}) {
    const textColor = `text-cyan-800`

    const [updateMode, setUpdateMode] = useState(false);

    const [governorateDC, setGovernorateDeliveryCompany] = useState(governorateDeliveryCompany);
    const id = governorateDC.governorateDeliveryCompanyId;

    const handleClick = () => {
        if (updateMode && updateModeId == id) {
            setUpdateMode(false);
            setUpdateModeId(-1);
        } if (updateMode && updateModeId != id) {
            setUpdateModeId(id);

        } else {
            setUpdateMode(!updateMode);
            setUpdateModeId(id);
        }
    }

    const iconStyle = (updateMode && updateModeId == id) ? ` text-green-800 bg-green-100 shadow-green-900`
        : `text-cyan-700 bg-cyan-100 shadow-cyan-900`
        ;
    return <div className={`flex flex-row items-center justify-between
                            ${governorateDC.deliveryCost == 0 || governorateDC.deliveryCost == null? ' bg-gradient-to-l from-red-50 to-red-100' : ` bg-gradient-to-l from-gray-50 to-sky-100`}
                             p-4 rounded-xl shadow-md/50 shadow-cyan-700`}>
        <NavLink to={""} className={`${textColor} hover:font-bold`} >{governorateDeliveryCompany.name}</NavLink>

        <div className="flex flex-row space-x-2 items-center">
            {!(updateMode && updateModeId == id) ?
                <p className={`${governorateDC.deliveryCost == 0 || governorateDC.deliveryCost == null? 'text-red-500' : textColor} font-bold `} >{governorateDC.deliveryCost ?? 0} </p>
                : <GovernorateDeliveryCompanyEdit setGovernorateDeliveryCompany={setGovernorateDeliveryCompany} setUpdateMode={setUpdateMode} governorateDeliveryCompany={governorateDC}></GovernorateDeliveryCompanyEdit>}
            <p>  ج.م  </p>
        </div>

        <span onClick={handleClick}
            className={`${iconStyle} flex-end cursor-pointer rounded-full  p-1 shadow-sm/50 `}>
            <MdModeEditOutline ></MdModeEditOutline>
        </span>
    </div>
}


export function GovernorateDeliveryCompanyEdit({
    setUpdateMode,
    setGovernorateDeliveryCompany,
    governorateDeliveryCompany
}: {
    setUpdateMode: React.Dispatch<SetStateAction<boolean>>,
    setGovernorateDeliveryCompany: React.Dispatch<SetStateAction<GovernorateDeliveryCompany>>,
    governorateDeliveryCompany: GovernorateDeliveryCompany
}) {


    const [value, setValue] = useState(governorateDeliveryCompany.deliveryCost ?? 0);

    const handleSubmit = async (e) => {

        e.preventDefault();
        setUpdateMode(false);
        if (value > 0) {
            const request = {
                GovernorateDeliveryCompanyId: governorateDeliveryCompany.governorateDeliveryCompanyId,
                DeliveryCost: value,
                IsActive: true
            }
            const { data }: { data: APIResponse<GovernorateDeliveryCompany> } = await axios.put(
                `${ApiLinks.deliveryCompany.updateGovernorateCost(governorateDeliveryCompany.deliveryCompanyId)}`
                , request)

            console.log(data);
            if (data.statusCode == 200) setGovernorateDeliveryCompany(data.result!);
        }


    }

    return <form onSubmit={handleSubmit} className=" ">
        <InputNumber defaultStyle={false} value={value > 0 ? value : ''} val={setValue} styles="text-center font-semibold text-blue-800 bg-gray-200 px-2  rounded-xl shadow-sm/50" ></InputNumber>
    </form>
}


export function DeliveryCompanyInfo({ deliveryCompany }: { deliveryCompany: DeliveryCompany; }) {
    // deliveryCompany.isActive = false;

    const isActive = deliveryCompany.isActive ? <p className="text-teal-800 bg-teal-100 font-bold  rounded-md p-2 shadow-md border-1  shadow-teal-200"><MdCheck className={'font-extrabold'}></MdCheck></p>
        : <p className="text-rose-800 bg-rose-100 font-bold rounded-xl p-2 shadow-md border-1  shadow-rose-200 "> <MdClose></MdClose>  </p>;

    return <div>
        <div className="flex flex-row justify-evenly     ">
            <h1 className="text-2xl font-extrabold text-blue-900">
                {deliveryCompany.name}
            </h1>
            {isActive}
        </div>
    </div>;
}
 
export function DeliveryCompanyGovernorateOrders() {
    return <div></div >
}

export function DeliveryCompanyGovernorateCost() {
    return <div></div >
} 
