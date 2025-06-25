import { useReducer, useState } from "react";
import { MdAddCircleOutline } from "react-icons/md";

import axios from "axios";
import Button from "../../../../../Components/ReuseableComponents/Button";
import ApiLinks from "../../../../../APICalls/ApiLinks";

import type { APIResponse } from "../../../../../app/Models/APIResponse";
import type { ProductQuantityPriceReadDTO } from "../../../../../app/Models/ProductQuantityPriceReadDTO";
import type { ProductQuantityPriceCreateDTO } from "../../../../../app/Models/ProductQuantityPriceCreateDTO";



const CHANGE_QUANTITY = 'change-quantity'
const CHANGE_UNIT_PRICE = 'change-unitPrice'
const CHANGE_NOTE = 'change-note'

interface ProductQuantityAction { type: string, payload: number | string | null }
const reducer = (state: ProductQuantityPriceCreateDTO, action: ProductQuantityAction): ProductQuantityPriceCreateDTO => {
    switch (action.type) {
        case CHANGE_QUANTITY:
            return {
                ...state,
                quantity: action.payload as number
            };
        case CHANGE_UNIT_PRICE:
            return {
                ...state,
                unitPrice: action.payload as number
            };
        case CHANGE_NOTE:
            return {
                ...state,
                note: action.payload as string | null
            }
        default:
            return {
                ...state
            }
    }
}

export default function ProductQuantityPriceForm({ quantity, unitPrice, productId, created, formKey, minimumPrice, maximumPrice }:
    { productId: string | undefined, quantity?: number, unitPrice: number, created: (x: number) => void, formKey: number, minimumPrice: number, maximumPrice: number }) {

    const [state, dispatch] = useReducer(reducer, {
        isActive: true,
        productId: Number(productId),
        quantity: quantity as number || 0,
        note: null,
        unitPrice: unitPrice as number || minimumPrice
    }
    );



    const [errMsg, setErrMsg] = useState(false);

    const inRange = (val: number) => val >= minimumPrice && val <= maximumPrice;


    const handlePriceChange = (event) => {
        const val: number = event.target.value;
        // setUnitPrice(val)
        dispatch(
            {
                type: CHANGE_UNIT_PRICE,
                payload: val
            }
        )

        if (inRange(val)) {
            setErrMsg(false);

        } else
            setErrMsg(true);
        console.log(val)
    }


    const handleQuantityChange = (event) => {
        const val: number = parseInt(event.target.value);
        dispatch(
            {
                type: CHANGE_QUANTITY,
                payload: val > 0 ? val : 0
            }
        )
    }


    const handleNoteChange = (event) => {
        const val: string = event.target.value;

        dispatch({
            type: CHANGE_NOTE,
            payload: val
        })
    }

    const inputStyle = `w-full md:w-1/2 p-3 bg-white rounded-md shadow-md/30`;

    const ErrMsg = <p className=" text-center text-red-500 bg-red-100 shadow rounded-sm p-3">
        <span className="text-xl m-2">
            *
        </span>
        السعر لازم يكون بين الحد الادنى
        <span className="m-1 text-red-800 font-bold">{minimumPrice}</span>
        والحد الاقصى
        <span className="m-1 text-red-800 font-bold">{maximumPrice}</span>
    </p>


    const handleSubmit = async (event) => {
        event.preventDefault();

        const { data }: APIResponse<ProductQuantityPriceReadDTO> = await axios.post(ApiLinks.productQuantityPrice.createQuantitiesPrices, state);
        console.log(data);

        created(formKey);
    }



    return <div className="flex flex-col gap-8 shadow-md/20 rounded-md m-3 p-3 bg-gradient-to-l from-indigo-300 to-violet-400">

        <div className="flex flex-col">
            <h1 className="text-2xl text-violet-800 font-bold">العرض</h1>
            <h2 className={`${errMsg ? 'text-rose-500' : 'text-indigo-900'} text-xl `}>سعر الوحده عند كمية معينة</h2>
        </div>

        {errMsg && ErrMsg}

        <form onSubmit={handleSubmit} className="space-y-8 flex flex-col items-center">

            <div className="flex flex-col  md:flex-row gap-3 items-center w-full justify-between">
                <input type="number" name="quantity" value={state.quantity || ''} id="quantity" placeholder="الكمية" onChange={handleQuantityChange} className={`${inputStyle}`} />

                <input value={state.unitPrice} onChange={handlePriceChange} type="number" name="unitPrice" id="unitPrice" placeholder="سعر الوحدة عند الكمية" className={`${errMsg ? 'text-rose-500' : 'text-indigo-900'} ${inputStyle}`} />

                <textarea onChange={handleNoteChange} placeholder="ملاحظات" name="note" value={!state.note ? '' : state.note} id="note" className={`${inputStyle}`}></textarea>
            </div>

            <div className="w-full rounded-xl shadow-md/30   border-2 border-violet-200 flex flex-col  bg-violet-300 overflow-hidden text-center">
                <p className="bg-violet-800 text-xl font-bold  text-violet-200  p-2">
                    إجمالى سعر العرض
                </p>
                <p className={`${errMsg ? 'text-rose-600' : `text-violet-900`} p-3 font-bold text-xl `}>
                    {!errMsg ? state.unitPrice * state.quantity : 0}
                </p>
            </div>

            <Button styles="flex fle-row space-x-5 justify-between items-center" primary hover>
                <span className="text-center font-bold">
                    اضف  السعر
                </span>
                <MdAddCircleOutline></MdAddCircleOutline>
            </Button>
        </form>
    </div>
}