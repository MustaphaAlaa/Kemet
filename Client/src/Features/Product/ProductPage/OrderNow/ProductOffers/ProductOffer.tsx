import { MdCheck, MdOutlineQuestionMark } from "react-icons/md"
import OpenClosedLabel from "../../../../../Components/ReuseableComponents/OpenClosedLabel"
import type { ProductQuantityPriceReadDTO } from "../../../../../app/Models/Product/ProductQuantityPriceReadDTO"

function ProductOffer({ productOffer,
    setExpandedIndex,
    expandedIndex,

}: {
    productOffer: ProductQuantityPriceReadDTO,
    setExpandedIndex: React.Dispatch<React.SetStateAction<number>>,
    expandedIndex: number
}) {
    const handleClick = () => expandedIndex == productOffer.productQuantityPriceId ? setExpandedIndex(-5) : setExpandedIndex(productOffer.productQuantityPriceId);
    return (

        <OpenClosedLabel
            valueToExpand={productOffer.productQuantityPriceId}
            expandedValue={expandedIndex}
            onClick={handleClick}
            title={`
                        اشتري
                        ${productOffer.quantity}
                        بنطلون 
                        بخصم 
                        ${productOffer.unitPrice}



                    `}
            icons={{
                hover: <MdCheck className={`  border-2 border-purple-800  bg-yellow-200 text-purple-800`} />,
                expanded: <MdCheck className={`   `} />,
                idle: <MdOutlineQuestionMark className={` `} />
            }}

            theme={{
                collapsed: `bg-gradient-to-br from-yellow-300 via-yellow-100 to-yellow-200 text-indigo-900 font-bold`,
                expanded: 'bg-gradient-to-bl from-teal-300  via-teal-400 to-teal-200 text-green-950 font-bold',
                hover: 'hover:bg-rose-100 hover:from-teal-100 hover:via-teal-200 hover:to-green-200 hover:font-bold'
            }}
        >
            <div className="">
                Hey you out there.

            </div>
        </OpenClosedLabel>


    )
}

export default ProductOffer
