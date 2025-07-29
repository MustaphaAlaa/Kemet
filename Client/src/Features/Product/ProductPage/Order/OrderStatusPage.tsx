import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ResponsiveSidebar } from "../../../../Components/ReuseableComponents/re";
import OrderCard from "./OrderCard";
import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO";
import Table, { type TableConfig } from "../../../../Components/ReuseableComponents/Table";
import Orders from "./Orders";
import axios from "axios";
import ApiLinks from "../../../../APICalls/ApiLinks";



interface LinkItem {
    id: number;
    label: string;
    component: React.ReactNode | null;
}

const links: LinkItem[] = [
    { id: 1, label: 'معلق', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
    { id: 2, label: 'المعالجة', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
    { id: 3, label: 'تم شحنها', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
    { id: 4, label: 'تم التوصيل', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
    { id: 5, label: 'تم الالغاء بواسطة العميل', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
    { id: 6, label: 'تم الإلغاء بواسطة الشركة', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
    { id: 7, label: 'تم استرداد المبلغ', component: 'lorem lgmfsklgnfdklnfkl fdgio fiajio oaoij oajr oaih oiha' },
]




export default function OrderStatusPage() {




    const { productId } = useParams<{ productId: string }>();
    console.log('productId', productId);


    const [selected, setSelected] = useState(1);
    const [orders, setOrders] = useState<OrderInfoDTO[]>([]);



    const hover = ` cursor-pointer hover:text-sky-800 hover:font-bold  `;
    const transStyle = `   hover:-translate-x-1 ease-in-out duration-300 transition-transform`;



    const linksArr = links.map(item => <a key={item.id} onClick={(e) => {
        e.preventDefault();
        setSelected(item.id);
        handleLinkClick(item.id);
    }}
        className={(selected == item.id ? 'text-rose-500  font-bold' : '') + `${transStyle} ${hover}   flex-shrink-0 `}>
        {item.label}
    </a>);

    useEffect(() => {

        const fetchOrders = async () => {
            // Use selected as the status parameter for fetching orders
            const { data } = await axios.get(`${ApiLinks.orders.ordersForStatus(parseInt(productId!), selected, 1)}`);
            setOrders(data.result);
        };

        fetchOrders();
        console.log(selected);
    }, [selected]);

    const handleLinkClick = (id: number) => {

        console.log(id)

    }

    const removeOrderFromJson = (orderId: number) => {
        const newOrders = orders.filter(item => item.orderId != orderId);
        setOrders(newOrders);
    }

    const lg = `lg:h-auto  lg:justify-normal lg:mx-auto  lg:space-y-3 lg:items-start   lg:flex-col lg:h-full lg:space-y-5 lg:rounded-tl-xl`;



    return (
        <div className="mt-3 w-screen  flex  flex-col  lg:grid md:grid-cols-12   gap-2 h-full xl:gap-8 ">
           
            <div className={`shadow-md/30 lg:col-span-3 xl:col-span-2 border-l-1 border-sky-300 p-8 lg:p-2 bg-gradient-to-t from-white   to-blue-200    overflow-x-scroll lg:overflow-x-auto`}>
                <div className={`  flex flex-row justify-between items-center  space-x-8 ${lg}`}>

                    {linksArr}
                </div>

            </div>
            
            <div className="xl:col-span-10 xl:col-start-3  lg:col-span-9 lg:col-start-5       ">
                    {selected == 3 ? 'de':''}
                <Orders orderInfoDTOs={orders} removeOrderFromJson={removeOrderFromJson}></Orders>

            </div >
        </div >
    )
} 