import { useEffect, useState } from "react";
import type { PriceReadDTO } from "../../../../../app/Models/PriceReadDTO";
import GetData from "../../../../../APICalls/GetData";
import ApiLinks from "../../../../../APICalls/ApiLinks";
import PriceRanges from "./PriceRanges";

export function ProductPriceRange({ productId }: { productId: string | undefined }) {


    const { data: price } = GetData<PriceReadDTO>(`${ApiLinks.price.get}/${productId}`);

    console.log(price);

    const [minimumPrice, setMinimumPrice] = useState(price?.minimumPrice);
    const [maximumPrice, setMaximumPrice] = useState(price?.maximumPrice);


    useEffect(() => {

        setMinimumPrice(price?.minimumPrice ?? 0);
        setMaximumPrice(price?.maximumPrice ?? 0);

    }, [price]);

    const priceRowStyle = `font-bold flex flex-col md:flex-row gap-4 justify-between items-center shadow-xl/30 rounded-xl border-2   p-3 bg-gradient-to-l  `;
 
    return <div className="grid grid-rows-auto  xl:grid-cols-2   gap-5 ">
        <div className={`${priceRowStyle}   text-lime-800   to-lime-400 from-lime-100  `}>
            <PriceRanges priceValue={minimumPrice} label={"الحد الادنى للسعر"}   ></PriceRanges>
        </div>
        <div className={`${priceRowStyle} from-rose-100 to-rose-400 text-rose-800  `}>
            <PriceRanges priceValue={maximumPrice} label={"الحد الاقصى للسعر"}   ></PriceRanges>
        </div>
    </div>
}

