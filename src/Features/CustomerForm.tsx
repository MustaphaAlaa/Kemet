import { useState } from "react";
import NotFirstTimeForm from "../Components/CustomerInfo/NotFirstTimeForm";
import FirstTimeForm from "../Components/CustomerInfo/FirstTimeForm";
import OpenClosedLabel from "../Components/ReuseableComponents/OpenClosedLabel";

export default function CustomerForm() {
    const [expandedValue, setExpandedIndex] = useState("");

    const handleClick = (index: string): void => index === expandedValue ? setExpandedIndex("") : setExpandedIndex(index);
     const notFirstTime = "notFirstTime";
    const firstTime = "firstTime";

    const formStyle = 'bg-indigo-100 rounded-b-sm  shadow-xl/20 p-3';
 

    
    // return <div className="mt-9  text-center flex flex-col sm:items-center sm:gap-4 sm:grid sm:items-start sm:grid-cols-1 sm:grid-cols-6  sm:grid-rows-3   auto-rows-max ">
        // return <div className="mt-9 text-center grid grid-cols-1 sm:grid-cols-6 gap-4 grid-flow-row-dense auto-rows-auto">
        return <div className="mt-9 text-center flex flex-col gap-5 items-center">

        <h3 className="text-3xl bg-indigo-100 font-bold sm:text-4xl md:text-5xl p-2 sm:p-3 md:p-4 xl:p-5 shadow-md/30 text-indigo-900"> اول مره تشتري من عندنا<span className="text-4xl text-purple-800 m-1">؟</span></h3>

        <div className="shadow-xl/30" >
            <OpenClosedLabel valueToExpand={firstTime} expandedValue={expandedValue} click={() => handleClick(firstTime)}>
                <label htmlFor="yes" >نعم, اول مره</label>
            </OpenClosedLabel>

            {(firstTime === expandedValue) && <FirstTimeForm formStyle={formStyle}></FirstTimeForm>}
        </div>
        <div className="shadow-xl/30">
            <OpenClosedLabel valueToExpand={notFirstTime} expandedValue={expandedValue} click={() => handleClick(notFirstTime)}>
                <label htmlFor="no">لا, ليس اول تعامل لي</label>
            </OpenClosedLabel>

            {(notFirstTime === expandedValue) && <NotFirstTimeForm formStyle={formStyle}></NotFirstTimeForm>}
        </div>
    
      
     </div>



 

}
