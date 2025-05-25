import { useState } from "react";
import NotFirstTimeForm from "../app/Components/CustomerInfo/NotFirstTimeForm";
import FirstTimeForm from "../app/Components/CustomerInfo/FirstTimeForm"; 
import OpenClosedLabel from "../app/Components/ReuseableComponents/OpenClosedLabel"; 

export default function CustomerForm() {
    const [expandedValue, setExpandedIndex] = useState("");

    const handleClick = (index: string): void => index === expandedValue ? setExpandedIndex("") : setExpandedIndex(index);

    // const iconStyle = " text-2xl  rounded-full";
    const notFirstTime = "notFirstTime";
    const firstTime = "firstTime";


    return <div className="  text-center items-center flex flex-col">
        <h3 className="mb-3 text-xl">اول مره تشتري من عندنا</h3>

        <div className="mb-2  ">
            <OpenClosedLabel valueToExpand={firstTime} expandedValue={expandedValue} click={() => handleClick(firstTime)}>
                <label htmlFor="yes">نعم, اول مره</label>
            </OpenClosedLabel>

            {(firstTime === expandedValue) && <FirstTimeForm  ></FirstTimeForm>}
        </div>
        <div className="mb-1 shadow-md">
            <OpenClosedLabel valueToExpand={notFirstTime} expandedValue={expandedValue} click={() => handleClick(notFirstTime)}>
                <label htmlFor="no">لا, ليس اول تعامل لي</label>
            </OpenClosedLabel>

            {(notFirstTime === expandedValue) && <NotFirstTimeForm></NotFirstTimeForm>}
        </div>
    </div>


}
