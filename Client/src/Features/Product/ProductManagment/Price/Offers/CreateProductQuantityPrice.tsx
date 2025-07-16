import { useParams } from "react-router-dom";
import type { PriceReadDTO } from "../../../../../app/Models/PriceReadDTO";
import ApiLinks from "../../../../../APICalls/ApiLinks";
import GetData from "../../../../../APICalls/GetData";
import React, { useEffect, useState, type ReactNode } from "react";
import Button from "../../../../../Components/ReuseableComponents/Button";
import { MdAddCircle } from "react-icons/md";
import ProductQuantityPriceForm from "./ProductQuantityPriceForm";
import type { ProductQuantityPriceReadDTO } from "../../../../../app/Models/Product/ProductQuantityPriceReadDTO";

export default function CreateProductQuantityPrice() {

    const { productId } = useParams();



    const [quantityCount, setQuantityCount] = useState(1);
    const { data: price } = GetData<PriceReadDTO>(`${ApiLinks.price.get}/${productId}`);


    const [qPricesNode, setQPricesNode] = useState<ReactNode[]>([]);
    const [quantityPricesNodes, setQuantityPricesNodes] = useState<ReactNode[]>([]);
    const { data: quantityPrices } = GetData<ProductQuantityPriceReadDTO[]>(`${ApiLinks.productQuantityPrice.quantitiesPrices}/${productId}`);


    const submitted = (formKey: number) => {


        setQPricesNode(prev => prev.filter(item => item.props.formKey != formKey));

    }

    const handleClick = () => {
        setQuantityCount(quantityCount + 1);
        console.log(`quantityCount: ${quantityCount}`)
        setQPricesNode([...qPricesNode, <ProductQuantityPriceForm productId={productId} formKey={quantityCount} created={submitted} maximumPrice={price.maximumPrice} minimumPrice={price.minimumPrice} key={quantityCount} />])


        console.log(`i'm Clicked`)


    }


    useEffect(() => { 

        if (quantityPrices != null || quantityPrices != undefined) {
            const quantities = quantityPrices?.map((item) => {
                console.log('sss')
                return <ProductQuantityPriceForm
                    productId={productId} created={submitted}
                    formKey={item.productQuantityPriceId} minimumPrice={price.minimumPrice} maximumPrice={price.maximumPrice}></ProductQuantityPriceForm>
            });
            setQuantityPricesNodes(quantities);
        }
console.log(`nooooooooooooooooode`)
            console.log(quantityPricesNodes)

    }, [quantityPrices])

    return <div className="grid grid-rows-auto md:col-span-2">


        <div>
            <Button onClick={handleClick} success hover className="flex flex-row gap-5" >
                <span>أضافة عرض</span>
                <span><MdAddCircle></MdAddCircle></span>
            </Button>
        </div>
        <div>
            
            {...quantityPricesNodes}
            {...qPricesNode}

        </div>

    </div>
}

