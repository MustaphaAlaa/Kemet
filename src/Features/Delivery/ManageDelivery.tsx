
import { useState } from "react";
import CreateDeliveryCompany from "./DeliveryCompany/CreateDeliveryCompany";
import EditDeliveryCompany from "./DeliveryCompany/EditDeliveryCompany";
import DeliveryCompaniesList from "./DeliveryCompany/DeliveryCompaniesList";


interface LinkItem {
    id: number;
    label: string;
    component: React.ReactNode | null;
}

const links: LinkItem[] = [{ id: 1, label: 'جميع المحافظات', component: '' },
{ id: 2, label: 'اسعار شحن المحافظات', component: '' },
{ id: 3, label: 'شركات الشحن', component: <DeliveryCompaniesList></DeliveryCompaniesList> },
 { id: 4, label: "إضافة شركة شحن", component: <CreateDeliveryCompany></CreateDeliveryCompany> },
{ id: 5, label: "تعديل شركات شحن", component: <EditDeliveryCompany></EditDeliveryCompany> },
]

const deliveryCompanyLinksId = [4, 5];


export default function ManageDelivery() {



    const [showSpecialLinks, setShowSpecialLinks] = useState<boolean>(false);
    const [selected, setSelected] = useState(-1);



    const hover = ` cursor-pointer hover:text-sky-500 hover:font-bold hover:border-l-3 `;
    const transStyle = `   hover:-translate-x-1 ease-in-out duration-300 transition-transform`;



    const visibleLinks = links.filter(link => !deliveryCompanyLinksId.includes(link.id) || showSpecialLinks)
    const linksArr = visibleLinks.map(item => <a key={item.id} onClick={(e) => {
        e.preventDefault();
        setSelected(item.id);
        handleLinkClick(item.id);
    }}
        className={(selected == item.id ? 'text-rose-500 border-l-3' : '') + `${transStyle} ${hover} `}>
        {item.label}
    </a>);



    const handleLinkClick = (id: number) => {

        console.log(selected)
        if (showSpecialLinks && id === 3)
            setShowSpecialLinks(false);
        else if (id === 3 || deliveryCompanyLinksId.includes(id))
            setShowSpecialLinks(true);
        else
            setShowSpecialLinks(false);

    }



    return (
        <div className="w-full mt-[2rem]     grid grid-cols-12  h-screen gap-2 ">
            <div className="hidden mr-[2rem] space-y-3 md:flex md:flex-col col-span-2 ">

                {linksArr}

            </div>
            <div className="  col-span-10 col-start-3     ">
                {links.find(item => item.id == selected)?.component}
            </div>

        </div >
    )
}
