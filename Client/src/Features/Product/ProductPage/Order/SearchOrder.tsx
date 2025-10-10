import { useEffect, useState } from "react";
import ApiLinks from "../../../../APICalls/ApiLinks";
import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO";
import OrderCard from "../../../Orders/OrderCard";
import { privateApi } from "../../../../APICalls/privateApi";


export default function SearchOrder() {

    const [searchTerm, setSearchTerm] = useState('');
    const [suggestions, setSuggestions] = useState<OrderInfoDTO[]>();
    const [isLoading, setIsLoading] = useState(false);


    useEffect(() => {
        if (searchTerm.length < 2) {
            setSuggestions(undefined);
            setIsLoading(false);
            return;
        }

        setIsLoading(true);

        const getSuggestions = async () => {
            const { data } = await privateApi.get(ApiLinks.orders.SearchOrder(searchTerm));
            setSuggestions(data?.result ?? []);
            setIsLoading(false);
        }



        const delayDebounce = setTimeout(() => {
            getSuggestions();

        }, 300);



        return () => clearTimeout(delayDebounce);
    }, [searchTerm]);

    const sug = suggestions?.map((item) => (
        <OrderCard key={item.orderId} orderInfoDTO={item}></OrderCard>
    ));


    return (
        <div className="flex flex-col space-y-8 relative">
            <input
                type="text"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                placeholder="Search by code..."
                className="w-full p-5 text-center rounded-xl shadow-xl/20 font-bold border-3 text-indigo-800 text-xl bg-white"
            />


            {/* Container with fixed height to prevent layout shifts */}
            <div className="min-h-0">
                {isLoading && (
                    <div className="bg-blue-100 text-blue-800 p-3 rounded-xl border-3 text-xl font-extrabold text-center">
                        Loading...
                    </div>
                )}

                {!isLoading && suggestions !== undefined && (
                    suggestions?.length > 0 ? (
                        <div className="max-h-130 overflow-y-auto">
                            <ul className="space-y-2">
                                {sug}
                            </ul>
                        </div>
                    ) : (
                        <div className="bg-rose-300 text-rose-800 p-3 rounded-xl border-3 text-xl font-extrabold">
                            There's no orders for this code
                        </div>
                    )
                )}
            </div>

        </div>
    )
}
