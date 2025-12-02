import GetData from '../../../../../APICalls/GetData'
import type { ProductQuantityPriceReadDTO } from '../../../../../app/Models/Product/ProductQuantityPriceReadDTO'
import ApiLinks from '../../../../../APICalls/ApiLinks'
import ProductOffer from './ProductOffer';
import {  useState } from 'react';

export default function ProductOffers({ productId }: { productId: number | string }) {

  const { data } = GetData<ProductQuantityPriceReadDTO[]>(ApiLinks.productQuantityPrice.quantitiesPrices(productId));



  const [expandedIndex, setExpandedIndex] = useState(-5);

  const offers = data?.map(offer => {
    // return <div className='m-1 border-2 border-sky-500'>
    return <div className='m-3 shadow-md/50'>
      <ProductOffer

        productOffer={offer}
        expandedIndex={expandedIndex}
        setExpandedIndex={setExpandedIndex}
      ></ProductOffer>
    </div>
  })



  console.log(data);
  return (
    // <div className={`  bg-white shadow-md/50 p-1   border-sky-500 border-2`}>
    <div className={``}>
      {offers}
      {/* <div className='flex flex-row'>
        <p>buy 5 paints by 500 L.E</p>
        <p className='bg-rose-400 text-rose-800 font-bold'>Save 500</p>

      </div> */}
    </div>
  )
}
