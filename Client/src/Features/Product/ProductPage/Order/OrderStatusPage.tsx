import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO";
import Orders from "./Orders";
import ApiLinks from "../../../../APICalls/ApiLinks";
import type PaginatedResult from "../../../../app/Models/PaginatedResult";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import Pagination from "../../../../Components/ReuseableComponents/Pagination/Pagination";
import { privateApi } from "../../../../APICalls/privateApi";

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



    const hover = ` cursor-pointer hover:text-sky-800 hover:font-bold hover:bg-sky-200 `;
    const transStyle = `   hover:-translate-x-1 ease-in-out duration-300 transition-transform`;



    const linksArr = links.map(item => <a key={item.id} onClick={(e) => {
        e.preventDefault();
        setSelected(item.id);
        handleLinkClick(item.id);
    }}
        className={(selected == item.id ? 'bg-rose-600 text-rose-200  font-bold' : '') +
            `${transStyle} ${hover}
          w-1/3 xl:w-full border-1 rounded-xl text-xl  bg-indigo-800 text-indigo-100 p-3 text- text-center  flex-shrink-0 
          shadow-md/50`}>
        {item.label}
    </a>);

    useEffect(() => {

        const fetchOrders = async () => {
            if (response != undefined || response?.data.length > 0) { setResponse(undefined) }

            setLoading(true);

            // Use selected as the status parameter for fetching orders
            try {

                const { data: response }: { data: APIResponse<PaginatedResult<OrderInfoDTO>> }
                    = await privateApi.get(ApiLinks.orders.ordersForStatus(parseInt(productId!), selected, currentPage, 10));
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

    const xl = `xl:h-auto  xl:justify-normal xl:mx-auto  xl:space-y-3 xl:items-start   xl:flex-col xl:h-full xl:space-y-5 xl:rounded-tl-xl`;

    console.log(`Should all orders should be here`);
    console.log(response);

    return (
        <>
            {/* Results Info */}
            {response && !loading && (
                <div className=" text-sm text-gray-600">
                    Showing {((response.currentPage - 1) * response.pageSize) + 1} to{' '}
                    {Math.min(response.currentPage * response.pageSize, response.totalCount)} of{' '}
                    {response.totalCount} results
                </div>
            )}
            <div className="mt-3    flex  flex-col  xl:grid xl:grid-cols-12   gap-2 h-full  overflow-hidden  ">

                <div className={`shadow-md/30 xl:col-span-2  flex-row  p-8 lg:p-2     overflow-x-scroll lg:overflow-x-auto`}>
                    <div className={` text-center flex flex-row justify-between items-center  space-x-8 ${xl}`}>

                        {linksArr}
                    </div>

                </div>


                <div className="xl:col-span-10 xl:col-start-3       ">
                    {/* Loading State */}
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




                    <Orders orderInfoDTOs={response?.data ?? []} removeOrderFromJson={removeOrderFromJson}></Orders>
                    <div>
                        <Pagination currentPage={currentPage} totalPages={response?.totalPages ?? 1} onPageChange={(page: number) => setCurrentPage(page)
                        } hasNext={response?.hasNext ?? false} hasPrevious={response?.hasPrevious ?? false}></Pagination>
                    </div>
                </div >
            </div >
        </>
    )
} 