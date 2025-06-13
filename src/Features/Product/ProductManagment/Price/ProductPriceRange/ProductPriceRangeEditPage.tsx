import { useEffect, useState } from "react";
import ApiLinks from "../../../../APICalls/ApiLinks";
import GetData from "../../../../APICalls/GetData";
import type { PriceReadDTO } from "../../../../app/Models/PriceReadDTO";
import { PriceRanges } from "./PriceRanges";
import Button from "../../../../Components/ReuseableComponents/Button";
import { MdAddCircle } from "react-icons/md";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import type { Price } from "../../../../app/Models/Price";
import { NavigationLinks } from "../../../../Navigations/NavigationLinks";

export default function ProductPriceRangeEditPage() {

    const navigate = useNavigate();



    const { productId } = useParams();
    const { data: price } = GetData<PriceReadDTO>(`${ApiLinks.price.get}/${productId}`);





    const [minimumPrice, setMinimumPrice] = useState(price?.minimumPrice);
    const [maximumPrice, setMaximumPrice] = useState(price?.maximumPrice);
    const [note, setNote] = useState(price?.note ?? '');




    useEffect(() => {

        setMinimumPrice(price?.minimumPrice ?? 0);
        setMaximumPrice(price?.maximumPrice ?? 0);
        setNote(price?.note ?? '');

    }, [price]);

    const priceRowStyle = `font-bold flex flex-col md:flex-row gap-4 justify-between items-center shadow-xl/30 rounded-xl border-2   p-3 bg-gradient-to-l  `;




    const handelOnChange = (event) => {
        const val = event.target.value;
        setNote(val);
    }

    const newPrice = {
        minimumPrice,
        maximumPrice,
        note,
        productId,
        isActive: true,
    };

    const updatePrice: Price = {
        minimumPrice,
        maximumPrice,
        note,
        productId,
        isActive: true,
        priceId: 0
    };




    const handleClick = async () => {



        if (price == null || price.minimumPrice != minimumPrice || price.maximumPrice != maximumPrice) {

            const { data } = await axios.post(`${ApiLinks.price.create}`, newPrice);
            console.log(`new price should be created`);
            console.log(data);
        } else if (price.note != note) {
            updatePrice.priceId = price.priceId;
            const { data } = await axios.put(`${ApiLinks.price.updateNote}`, updatePrice);
        }



        navigate(`${NavigationLinks.product.productPrice}/${productId}`);
        console.log('i am clicked #%#')

    }

    return <div className="grid grid-rows-auto  xl:grid-cols-2   gap-5 p-5 ">
        <div className={`${priceRowStyle}   text-lime-800   to-lime-400 from-lime-100  `}>
            {/* <PriceRanges priceValue={500} label={"الحد الادنى للسعر"} price={{ maximumPrice: 12, minimumPrice: 1, productId: 4 }} ></PriceRanges> */}
            <PriceRanges editMode priceValue={minimumPrice} label={"الحد الادنى للسعر"} setPrice={setMinimumPrice} ></PriceRanges>
        </div>
        <div className={`${priceRowStyle} from-rose-100 to-rose-400 text-rose-800  `}>
            {/* <PriceRanges priceValue={500} label={"الحد الاقصى للسعر"} price={{ maximumPrice: 12, minimumPrice: 1, productId: 4 }} ></PriceRanges> */}
            <PriceRanges editMode priceValue={maximumPrice} label={"الحد الاقصى للسعر"} setPrice={setMaximumPrice} ></PriceRanges>
        </div>

        <div className=" md:w-2/3  md:justify-self-center   md:col-span-2 ">
            <div className="  w-full  xl:w-3xl flex flex-col items-center justify-center rounded-xl shadow-xl/20 bg-gradient-to-l from-violet-300 to-purple-600 p-1">
                <p className="text-blue-800 font-bold my-3 text-xl">ملاحظات:</p>
                <textarea className="bg-white w-full xl:w-2xl mb-8 border-1 shadow-md/30 h-48 text-center rounded-xl focus:outline text-blue-800" value={note} onChange={handelOnChange}></textarea>
            </div>
        </div>

        <Button onClick={handleClick} success hover className={`md:col-span-2 justify-self-center   flex flex-row items-center justify-between gap-3 text-3xl`}>
            <span>حفظ</span>
            <span><MdAddCircle></MdAddCircle></span>
        </Button>
    </div>
}



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

