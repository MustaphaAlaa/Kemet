import { NavigationLinks } from "../../Navigations/NavigationLinks";
import { useReducer, useState, type ReactNode } from "react";
import CreateDeliveryCompany from "./DeliveryCompany/CreateDeliveryCompany";
import EditDeliveryCompany from "./DeliveryCompany/EditDeliveryCompany";

const CREATE_DELIVERY_COMPANY = 'add_delivery_company';
const EDIT_DELIVERY_COMPANY = 'edit_delivery_company';

interface IDeliveryManagementSidebar { type: string, payload?: ReactNode }


const links = [{ id: 1, label: 'جميع المحافظات', component: '' },
{ id: 2, label: 'اسعار شحن المحافظات', component: '' },
{ id: 3, label: 'شركات الشحن', component: '' },
]

const reducer = (state, action: IDeliveryManagementSidebar) => {
    switch (action.type) {
        case CREATE_DELIVERY_COMPANY:
            return <CreateDeliveryCompany></CreateDeliveryCompany>
        case EDIT_DELIVERY_COMPANY:
            return <EditDeliveryCompany></EditDeliveryCompany>
        default:
            return null;
    }
}

export default function ManageDelivery() {


    const [state, dispatch] = useReducer(reducer, <div></div>);

    const [selected, setSelected] = useState(-1);

    const deliveryCompanyLinks = [
        { id: 6, to: `${NavigationLinks.deliveryManagement.deliveryCompany.all}`, label: "جميع شركات الشحن", component: '' },
        { id: 7, to: `${NavigationLinks.deliveryManagement.deliveryCompany.create}`, label: "إضافة شركة شحن", component: CREATE_DELIVERY_COMPANY },
    ];

    const hover = ` cursor-pointer hover:text-sky-500 hover:font-bold hover:border-l-3 `;
    const transStyle = `   hover:-translate-x-1 ease-in-out duration-300 transition-transform`;



    const linksArr = links.map(item => <a key={item.id} onClick={(e) => {
        e.preventDefault();
        dispatch({ type: item.component });
        setSelected(item.id);
    }}
        className={(selected == item.id ? 'text-rose-500 border-l-3' : '') + `${transStyle} ${hover} `}>
        {item.label}
    </a>);



    const deliveryCompaniesLinks = selected == 3 || selected == 6 || selected == 7 ? deliveryCompanyLinks.map(item => <a
        onClick={(e) => {
            e.preventDefault();
            dispatch({ type: item.component });
            setSelected(item.id);

        }}

        className={(selected == item.id ? 'text-rose-500 border-l-3' : '') + `${transStyle} ${hover} mr-2`} key={item.label}>
        {item.label}
    </a>) : null

    return (
        <div className="w-full mt-[2rem]     grid grid-cols-12  h-screen gap-2 ">
            <div className="hidden mr-[2rem] space-y-3 md:flex md:flex-col col-span-2 ">

                {linksArr}

                {deliveryCompaniesLinks}


            </div>
            <div className=" shadow-md/50 rounded-tr-xl   col-span-10 col-start-3 bg-gradient-to-r from-10% from-teal-200 to-emerald-100 ">
                {state}
            </div>

        </div >
    )
}
