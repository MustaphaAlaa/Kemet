import { useState } from "react";
import { useParams } from "react-router-dom";
import { ResponsiveSidebar } from "../../../../Components/ReuseableComponents/re";



interface LinkItem {
    id: number;
    label: string;
    component: React.ReactNode | null;
}

const links: LinkItem[] = [
    { id: 1, label: 'معلق', component: '' },
    { id: 2, label: 'المعالجة', component: '' },
    { id: 3, label: 'تم شحنها', component: '' },
    { id: 4, label: 'تم التوصيل', component: '' },
    { id: 5, label: 'تم الالغاء بواسطة العميل', component: '' },
    { id: 6, label: 'تم الإلغاء بواسطة الشركة', component: '' },
    { id: 7, label: 'تم استرداد المبلغ', component: '' },
]

// const deliveryCompanyLinksId = [4, 5];


export default function OrderStatusPage() {




    const { productId } = useParams<{ productId: string }>();
    console.log('productId', productId);


    // const [showSpecialLinks, setShowSpecialLinks] = useState<boolean>(false);
    const [selected, setSelected] = useState(-1);



    const hover = ` cursor-pointer hover:text-sky-800 hover:font-bold md:hover:border-l-3 `;
    const transStyle = `   hover:-translate-x-1 ease-in-out duration-300 transition-transform`;



    // const visibleLinks = links.filter(link => !deliveryCompanyLinksId.includes(link.id) || showSpecialLinks)
    const linksArr = links.map(item => <a key={item.id} onClick={(e) => {
        e.preventDefault();
        setSelected(item.id);
        handleLinkClick(item.id);
    }}
        className={(selected == item.id ? 'text-rose-500 border-l-3 font-bold' : '') + `${transStyle} ${hover}  sm:px-4 px-0 `}>
        {item.label}
    </a>);



    const handleLinkClick = (id: number) => {

        // console.log(selected)
        // if (showSpecialLinks && id === 3)
        //     setShowSpecialLinks(false);
        // else if (id === 3 || deliveryCompanyLinksId.includes(id))
        //     setShowSpecialLinks(true);
        // else
        //     setShowSpecialLinks(false);

    }


    const lg = `lg:h-auto  lg:justify-normal  lg:space-y-3 lg:items-start lg:col-span-2`;
    const content =
        <div className="w-full flex flex-col  lg:grid md:grid-cols-12   gap-2 h-full ">
            <div className={` ${lg}   h-full  justify-center   space-y-5  flex flex-col  items-center     rounded-tl-xl  ${lg}  `}>

                {linksArr}

            </div>
            <div className="  col-span-10 col-start-3     ">
                {links.find(item => item.id == selected)?.component}
            </div>

        </div >;

    return <ResponsiveSidebar sidebarContent={<div className={` ${lg}   h-full  justify-center   space-y-5  flex flex-col  items-center     rounded-tl-xl  ${lg}  `}>

        {linksArr}

    </div>}>{1}</ResponsiveSidebar>
    // return (
    //     <div className="w-full flex flex-col  lg:grid md:grid-cols-12   gap-2 h-full ">
    //         <div className={` ${lg}   h-full  justify-center   space-y-5  flex flex-col  items-center  border-l-1 border-sky-300 p-2 bg-gradient-to-t from-white   to-blue-200  rounded-tl-xl  ${lg}  `}>

    //             {linksArr}

    //         </div>
    //         <div className="  col-span-10 col-start-3     ">
    //             {links.find(item => item.id == selected)?.component}
    //         </div>

    //     </div >
    // )
}