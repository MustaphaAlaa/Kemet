
import { useState } from "react";
import { NavLink, useLocation, useParams } from "react-router-dom";
import Button from "../../../../Components/ReuseableComponents/Button";
import axios from "axios";
import ApiLinks from "../../../../APICalls/ApiLinks";
import { NavigationLinks } from "../../../../Navigations/NavigationLinks";
import type { Product } from "../../../../app/Models/Product/Product";
import { ProductPriceRange } from "./ProductPriceRange/ProductPriceRange";
import ProductQuantityPricePage from "./Offers/ProductQuantityPricePage";
import { MdAddCircle } from "react-icons/md";

export default function ProductVariantPricePage() {

    const { productId } = useParams();

    const location = useLocation();

    const [product, setProduct] = useState<Product | null>(location.state?.product);

    const getProduct = async () => {
        const { data } = await axios.get(`${ApiLinks.product.get}/${productId}`)
        setProduct(data?.result);
    }

    if (!product) getProduct();


    const [productPriceRangeEditMode, setProductPriceRangeEditMode] = useState(false);

    const handleClick = () => setProductPriceRangeEditMode(!productPriceRangeEditMode);




    return <div className=" text-center mt-5 p-5 flex flex-col gap-5   ">
        <h1 className="text-gray-800 text-3xl">{product?.name}</h1>
        <div className="flex flex-row justify-between">

            <NavLink to={`${NavigationLinks.product.productPrice}/${productId}/editMode`}  >
                <Button primary hover onClick={handleClick}> تعديل حدود الاسعار </Button>
            </NavLink>


            <NavLink to={`${NavigationLinks.product.productQuantityPrice}/${productId}`} className={`self end`}>
                <Button success hover className={`flex flex-row items-center gap-3`}>
                    <span className="mb-1">
                        تعديل اسعار الكميات
                    </span>
                    <MdAddCircle></MdAddCircle>
                </Button>
            </NavLink>
        </div>


        <ProductPriceRange productId={productId}></ProductPriceRange>
        <div className="md:self-center md:w-1/2">
            <ProductQuantityPricePage></ProductQuantityPricePage>
        </div>
    </div>
} 