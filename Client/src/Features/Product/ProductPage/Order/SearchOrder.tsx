import { useEffect, useState } from "react";
import ApiLinks from "../../../../APICalls/ApiLinks";
import type { APIResponse } from "../../../../app/Models/APIResponse";
import type { OrderInfoDTO } from "../../../../app/Models/OrderInfoDTO";
import OrderCard from "../../../Orders/OrderCard";


export default function SearchOrder() {

    const [searchTerm, setSearchTerm] = useState('');
    const [suggestions, setSuggestions] = useState<OrderInfoDTO[]>();

    useEffect(() => {
        if (searchTerm.length < 2) {
            setSuggestions(undefined);
            return;
        }

        const getSuggestions = () => {
            fetch(`${ApiLinks.orders.SearchOrder(searchTerm)}`)
                .then(res => res.json())
                .then(res => { console.log(res); return res; })
                .then((data: APIResponse<OrderInfoDTO[]>) => setSuggestions(data?.result ?? []));
        }



        const delayDebounce = setTimeout(() => {
            getSuggestions();

        }, 300);



        return () => clearTimeout(delayDebounce);
    }, [searchTerm]);



    return (
        <div className="flex flex-col space-y-8">
            <input
                type="text"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
                placeholder="Search by code..."
                className="w-full p-5 rounded-xl shadow-xl/20 font-bold text-indigo-800 text-xl bg-white"
            />

            {suggestions != undefined ? (suggestions?.length > 0 ? <ul>
                {suggestions?.map((item ) => (
                    <OrderCard key={item.orderId} orderInfoDTO={item}></OrderCard>
                ))}
            </ul> : <div className="bg-rose-300 text-rose-800 p-3 rounded-xl border-3 text-xl font-extrabold">There's no orders for this code</div>) : ""}

        </div>
    )
}
