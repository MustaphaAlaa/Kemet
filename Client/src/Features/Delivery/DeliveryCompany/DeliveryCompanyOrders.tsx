import { useParams } from "react-router-dom";
import type { OrderInfoDTO } from "../../../app/Models/OrderInfoDTO";
import Pagination from "../../../Components/ReuseableComponents/Pagination/Pagination";
import { useEffect, useState } from "react";
import type PaginatedResult from "../../../app/Models/PaginatedResult";
import type { APIResponse } from "../../../app/Models/APIResponse";
import axios from "axios";
import ApiLinks from "../../../APICalls/ApiLinks";
import OrderCard from "../../Orders/OrderCard";
import { RiFileExcel2Fill } from "react-icons/ri";


export default function DeliveryCompanyOrders() {


    const { deliveryCompanyId } = useParams<string>();


    const [selectedOrdersId, setSelected] = useState(new Set<number>());
    const [response, setResponse] = useState<PaginatedResult<OrderInfoDTO> | undefined>(undefined);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [currentPage, setCurrentPage] = useState(1);




    useEffect(() => {

        const fetchOrders = async () => {
            if (response != undefined || response?.data.length > 0) { setResponse(undefined) }

            setLoading(true);
            try {

                const { data }: { data: APIResponse<PaginatedResult<OrderInfoDTO>> } = await axios.get(`${ApiLinks.deliveryCompany.getOrders(parseInt(deliveryCompanyId), currentPage, 30)}`);
                console.log(data)

                setResponse(data.result ?? undefined);

            } catch (err) {
                setError(err instanceof Error ? err.message : 'Failed to fetch products');

            } finally {

                setLoading(false);
            }

        };

        fetchOrders();
        console.log(selectedOrdersId);

    }, [currentPage]);



    useEffect(() => {

        const saved = localStorage.getItem('selectedOrders');

        if (saved) {
            setSelected(new Set(JSON.parse(saved)));
        }

    }, []);


    useEffect(() => {
        localStorage.setItem('selectedOrders', JSON.stringify(Array.from(selectedOrdersId)));
    }, [selectedOrdersId]);


    const toggleSelect = (id: number) => {
        setSelected(prevState => {
            const newSet = new Set<number>(prevState);
            if (newSet.has(id))
                newSet.delete(id);
            else
                newSet.add(id);
            return newSet;
        })
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

            <div className="mt-3 flex  flex-col p-3       ">
                {selectedOrdersId.size > 0 && <div
                    className="flex flex-row w-[5rem] border-2 rounded-xl justify-center text-3xl text-green-700 bg-white shadow-lg/40 shadow-green-700 py-3 my-4 cursor-pointer" >

                    <RiFileExcel2Fill className="" />
                </div>}
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


                    {response?.data.map(item =>
                        <div key={item.orderId} className="bg-blue-200 ">
                            <input type="checkbox" checked={selectedOrdersId.has(item.orderId)} onChange={() => toggleSelect(item.orderId)} />
                            <OrderCard orderInfoDTO={item}></OrderCard>
                        </div>
                    )}


                    <div>
                        <Pagination currentPage={currentPage} totalPages={response?.totalPages ?? 1} onPageChange={(page: number) => setCurrentPage(page)
                        } hasNext={response?.hasNext ?? false} hasPrevious={response?.hasPrevious ?? false}></Pagination>
                    </div>
                </div >
            </div >
        </>
    )
} 
