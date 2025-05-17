import { useState, type ChangeEvent, type FormEvent } from "react";
import NotFirstTimeForm from "../app/Components/CustomerInfo/NotFirstTimeForm";
import FirstTimeForm from "../app/Components/CustomerInfo/FirstTimeForm";
import { AiOutlineDownCircle, AiOutlineLeftCircle } from "react-icons/ai";
import OpenClosedLabel from "../app/Components/ReuseableComponents/OpenClosedLabel";

export default function FirstTimeBuy() {
    const [expandedValue, setExpandedIndex] = useState("");

    const handleClick = (index: string): void => index === expandedValue ? setExpandedIndex("") : setExpandedIndex(index);

    // const isExpended = expandedIndex.length > 0;



    // function handleSubmit(event: FormEvent<HTMLFormElement>): void {
    //     event.preventDefault();
    //     console.log(event)

    //     console.log("form is submitted")
    // }

    // function handelChange(event: ChangeEvent<HTMLInputElement>): void {
    //     if (event.target.value.length == 11)
    //         console.log(event.target.value)
    // }

    const iconStyle = " text-2xl  rounded-full";
    const notFirstTime = "notFirstTime"
    const firstTime = "firstTime"
    return <div className="text-center items-center flex flex-col">
        <h3  className="mb-3 text-xl">اول مره تشتري من عندنا</h3>

        <div className="mb-2 shadow-md">
            <OpenClosedLabel valueToExpand={firstTime} expandedValue={expandedValue} click={() => handleClick(firstTime)}>
                <label htmlFor="yes">نعم, اول مره</label>
            </OpenClosedLabel>

            {(firstTime === expandedValue) && <FirstTimeForm></FirstTimeForm>}

        </div>
        <div className="mb-1 shadow-md">
            <OpenClosedLabel valueToExpand={notFirstTime} expandedValue={expandedValue} click={() => handleClick(notFirstTime)}>
                <label htmlFor="no">لا, ليس اول تعامل لي</label>
            </OpenClosedLabel>


            {(notFirstTime === expandedValue) && <NotFirstTimeForm></NotFirstTimeForm>}
        </div>

    </div>


}
