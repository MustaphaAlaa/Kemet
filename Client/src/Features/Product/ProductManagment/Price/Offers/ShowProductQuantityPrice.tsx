import type { ProductQuantityPriceReadDTO } from "../../../../../app/Models/Product/ProductQuantityPriceReadDTO";




export default function ShowProductQuantityPrice({ productQuantityPrice }: { productQuantityPrice: ProductQuantityPriceReadDTO; }) {
    return <div className="flex flex-col border-2 border-white gap-3 bg-gradient-to-l to-blue-100 from-white rounded-md shadow-md/40 p-3">
       
       

        <div className={`flex flex-row font-bold   self-start  shadow-md/30`}>
            <p className="bg-violet-800 p-2 text-violet-100 rounded-tr-md rounded-br-md">
                الكمية
            </p>
            <p className="p-2 bg-violet-500">
                {productQuantityPrice.quantity}
            </p>

        </div>
        <div className=" flex flex-row justify-evenly">
            <p className="flex flex-col shadow-xl/30 shadow-violet-800 overflow-hidden rounded-md border-2  border-blue-600">
                <span className="p-1 px-5 bg-cyan-50 text-blue-600 font-extrabold">
                    سعر الوحدة
                </span>
                <span className="p-2 px-5 bg-sky-600 font-bold text-sky-50">
                    {productQuantityPrice.unitPrice} ج.م
                </span>
            </p>
            <p className=" flex flex-col shadow-md/50 shadow-teal-800  overflow-hidden rounded-md border-2  border-emerald-500">
                <span  className="p-1 px-5 bg-lime-50 text-emerald-700 text-shadow-lime-600 text-shadow-2xs  font-extrabold">
                    إجمالى العرض
                </span>
                <span className="p-2 px-5 bg-emerald-900 text-teal-100 font-extrabold">
                    {productQuantityPrice.unitPrice * productQuantityPrice.quantity} ج.م
                </span>
            </p>
        </div>

        <div className={` font-bold self-end bg-emerald-200 p-3`}>Qunatity3</div>
    </div>;
}
