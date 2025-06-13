import { MdAddCircle } from "react-icons/md";
import { useState, type ReactNode } from "react";
import { NavLink, useLocation, useParams } from "react-router-dom";
import type { ProductPriceOrchestratorCreateDTO } from "../../../../app/Models/ProductPriceOrchestratorCreateDTO";
import type { QuantityPrices } from "../../../../app/Models/QuantityPrices";
import Button from "../../../../Components/ReuseableComponents/Button";
import type { Product } from "../../../../app/Models/Product";
import axios from "axios";
import ApiLinks from "../../../../APICalls/ApiLinks"; 
import { NavigationLinks } from "../../../../Navigations/NavigationLinks";
import { ProductPriceRange } from "./ProductPriceRange/ProductPriceRangeEditPage";

export default function ProductVariantPricePage() {

    const { productId } = useParams();

    const location = useLocation();

    const [product, setProduct] = useState<Product | null>(location.state?.product);

    const getProduct = async () => {
        const { data } = await axios.get(`${ApiLinks.product.get}/${productId}`)
        setProduct(data?.result);
    }

    if (!product) getProduct();



    interface dProductPriceOrchestratorCreateDTO {
        minimumPrice: number,
        maximumPrice: number,
        startFrom?: Date,
        EndsAt?: Date,
        Note?: string,
        quantitiesPrices: QuantityPrices[]

        productId: number,
    }

    const [minimumPrice, setMinimumPrice] = useState(0);
    const [maximumPrice, setMaximumPrice] = useState(0);
    const [note, setNote] = useState('');






    const reqObj: ProductPriceOrchestratorCreateDTO = {
        minimumPrice,
        maximumPrice,
        quantitiesPrices: [{ quantity: 1, productQuantityPrice: { quantityPrice: 1 } }],
        productId: 1,
    }



    const [productPriceRangeEditMode, setProductPriceRangeEditMode] = useState(false);

    const handleClick = () => setProductPriceRangeEditMode(!productPriceRangeEditMode);



    return <div className=" text-center mt-5 p-5   ">
        <h1>{product?.name}</h1>
        <NavLink to={`${NavigationLinks.product.productPrice}/${productId}/editMode`}  >
            <Button primary hover onClick={handleClick}> تعديل حدود الاسعار </Button>
        </NavLink>
        <ProductPriceRange productId={productId}></ProductPriceRange>
    </div>


}


export function Qpp({ maximumPrice, minimumPrice }: { maximumPrice: number, minimumPrice: number }) {


    const qPrices: QuantityPrices[] = [];
    const [quantityCount, setQuantityCount] = useState(1);

    // const qPricesNode: ReactNode[] = []

    const [qPricesNode, setQPricesNode] = useState<ReactNode[]>([]);

    const handleClick = () => {
        setQuantityCount(quantityCount + 1);
        console.log(`quantityCount: ${quantityCount}`)
        setQPricesNode([...qPricesNode, <QuantityProductPrice maximumPrice={maximumPrice} minimumPrice={minimumPrice} key={quantityCount} />])
        console.log(`i'm Clicked`)
    }



    return <div className="grid grid-rows-auto md:col-span-2">
        <div>
            <Button onClick={handleClick} success hover className="flex flex-row gap-5" >
                <span>أضافة عرض</span>
                <span><MdAddCircle></MdAddCircle></span>
            </Button>
        </div>
        <div>
            {...qPricesNode}
        </div>
    </div>
}



export function QuantityProductPrice({ minimumPrice, maximumPrice }: { minimumPrice: number, maximumPrice: number }) {
    const handleSubmit = () => {

    }



    const [value, setValue] = useState(minimumPrice);

    const inRange = (val: number) => val >= minimumPrice && val <= maximumPrice;
    const handleChange = (event) => {
        const val: number = event.target.value;
        // if (inRange(val))
        console.log(val)
        setValue(val)
    }

    const inputStyle = `w-1/2 p-3 bg-white rounded-md shadow-md/30`
    const ErrMsg = <p className="text-red-500 bg-red-100 shadow rounded-sm p-1">
        * السعر لازم يكون بين الحد الادنى
        <span className="m-1 text-red-800 font-bold">{minimumPrice}</span>
        والحد الاقصى
        <span className="m-1 text-red-800 font-bold">{maximumPrice}</span>
    </p>

    return <div className="flex flex-col gap-8 shadow-md/20 rounded-md m-3 p-3 bg-gradient-to-l from-indigo-300 to-violet-400">

        <div className="flex flex-col">
            <h1 className="text-2xl text-violet-800 font-bold">العرض</h1>
            <h2 className="text-xl text-indigo-900">سعر الوحده عند كمية معينة</h2>
        </div>
        {ErrMsg}
        <form onSubmit={handleSubmit} className="">
            <div className="flex flex-col md:flex-row gap-3 items-center w-full justify-between">
                <div className="w-full">
                    <input type="number" name="quantity" id="quantity" placeholder="الكمية" className={`${inputStyle}`} />

                </div>
                <div className="w-full">
                    <input value={value} onChange={handleChange} type="number" name="quantity" id="quantity" placeholder="سعر الوحدة عند الكمية" className={`${inputStyle}`} />

                </div>
                <div className="w-full">
                    <input type="text" name="quantity" id="quantity" placeholder="الكمية" className={`${inputStyle}`} />

                </div>
            </div>
        </form>
    </div>
}