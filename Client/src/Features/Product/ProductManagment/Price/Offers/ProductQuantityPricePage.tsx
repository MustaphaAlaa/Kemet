import { NavLink, useParams } from "react-router-dom";
import ApiLinks from "../../../../../APICalls/ApiLinks";
import GetData from "../../../../../APICalls/GetData";
import type { ProductQuantityPriceReadDTO } from "../../../../../app/Models/Product/ProductQuantityPriceReadDTO";
import { MdAddCircle } from "react-icons/md";
import { NavigationLinks } from "../../../../../Navigations/NavigationLinks"; 
import Button from "../../../../../Components/ReuseableComponents/Button";
import ShowProductQuantityPrice from "./ShowProductQuantityPrice";

export default function ProductQuantityPricePage() {
    
    const { productId } = useParams();
 
    const { data: productQuantityPrices } = GetData<ProductQuantityPriceReadDTO[]>(`${ApiLinks.productQuantityPrice.quantitiesPrices}/${productId}`);
    
   const PQPs = productQuantityPrices?.sort((a,b)=> a.quantity - b.quantity)?.map(item => 
        <ShowProductQuantityPrice productQuantityPrice={item} key={item.productQuantityPriceId} ></ShowProductQuantityPrice  > 
    )


    
    //!!!! When ever go this page should be able to edit previous PQPs, of course in backend it'll be a new active record, and the record that edited it'll be deactivate 
  


    
    const AddOffersDiv = <div className="flex flex-col">
         <NavLink to={`${NavigationLinks.product.productQuantityPrice}/${productId}`} className={`self end`}>
            <Button success hover className={`flex flex-row items-center gap-3`}>
                <span className="mb-1">
                    أضف عرض
                </span>
                <MdAddCircle></MdAddCircle>
            </Button>
        </NavLink> 
        {PQPs && PQPs.length > 1 ? PQPs :    <p className="text-gray-700 text-2xl">
            لا يوجد عروض
        </p>} 
    </div>

    return <div className="space-y-5"> 
        {PQPs &&  PQPs.length < 1 ? AddOffersDiv : PQPs}
    </div>

}

