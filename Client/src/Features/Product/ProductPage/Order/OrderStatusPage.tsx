import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO";
import Orders from "./Orders";
import axios from "axios";
import ApiLinks from "../../../../APICalls/ApiLinks";
import type PaginatedResult from "../../../../app/Models/PaginatedResult";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import Pagination from "../../../../Components/ReuseableComponents/Pagination/Pagination";



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
    const [response, setResponse] = useState<PaginatedResult<OrderInfoDTO> | undefined>(undefined);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [currentPage, setCurrentPage] = useState(1);
    // const [pageSize, setPageSize] = useState(10);
    // const [totalPages, setTotalPages] = useState(0);
    // const [resp, setRes] = useState<APIResponse.>();



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
            if (response != undefined || response?.result?.data.length > 0) { setResponse(undefined) }

            setLoading(true);

            // Use selected as the status parameter for fetching orders
            try {

                const { data: response }: { data: APIResponse<PaginatedResult<OrderInfoDTO>> } = await axios.get(`${ApiLinks.orders.ordersForStatus(parseInt(productId!), selected, currentPage, 10)}`);
                setResponse(response.result ?? undefined);

            } catch (err) {
                setError(err instanceof Error ? err.message : 'Failed to fetch products');

            } finally {

                setLoading(false);
            }

        };

        fetchOrders();

        console.log(selected);
    }, [selected, currentPage]);

    const handleLinkClick = (id: number) => {

        console.log(id)

    }

    const removeOrderFromJson = (orderId: number) => {
        if (!response) return;
        const newOrders = response.data.filter(item => item.orderId != orderId);
        setResponse({ ...response, data: newOrders });
    }

    const lg = `lg:h-auto  lg:justify-normal lg:mx-auto  lg:space-y-3 lg:items-start   lg:flex-col lg:h-full lg:space-y-5 lg:rounded-tl-xl`;

    console.log(`Should all orders should be here`);
    console.log(response);
    function sleep(ms = 5000) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
    return (
        <div className="mt-3 w-screen  flex  flex-col  lg:grid md:grid-cols-12   gap-2 h-full xl:gap-8 ">

            <div className={`shadow-md/30 lg:col-span-3 xl:col-span-2 border-l-1 border-sky-300 p-8 lg:p-2 bg-gradient-to-t from-white   to-blue-200    overflow-x-scroll lg:overflow-x-auto`}>
                <div className={`  flex flex-row justify-between items-center  space-x-8 ${lg}`}>

                    {linksArr}
                </div>

            </div>

            {/* Loading State */}

            <div className="xl:col-span-10 xl:col-start-3  lg:col-span-9 lg:col-start-5       ">
                {loading && (
                    <div className="flex justify-center items-center py-12">
                        <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
                    </div>
                )}


                {/* Error State */}
                {error && (
                    <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-md mb-6">
                        {error}
                    </div>
                )}

                {/* Results Info */}
                {response && !loading && (
                    <div className="mb-4 text-sm text-gray-600">
                        Showing {((response.currentPage - 1) * response.pageSize) + 1} to{' '}
                        {Math.min(response.currentPage * response.pageSize, response.totalCount)} of{' '}
                        {response.totalCount} results
                    </div>
                )}


                <Orders orderInfoDTOs={response?.data ?? []} removeOrderFromJson={removeOrderFromJson}></Orders>
                <div>
                    <Pagination currentPage={currentPage} totalPages={response?.totalPages ?? 1} onPageChange={(page: number) => setCurrentPage(page)
                    } hasNext={response?.hasNext ?? false} hasPrevious={response?.hasPrevious ?? false}></Pagination>
                </div>
            </div >
        </div >
    )
} 