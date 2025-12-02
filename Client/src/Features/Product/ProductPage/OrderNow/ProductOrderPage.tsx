import { useState } from "react";
import FirstTimeForm from "../../../../Components/CustomerInfo/FirstTimeForm";
import OpenClosedLabel from "../../../../Components/ReuseableComponents/OpenClosedLabel";
import { TbTruckDelivery } from "react-icons/tb";
import { GrShop } from "react-icons/gr";
import ProductOffers from "./ProductOffers/ProductOffers";
import { useParams } from "react-router-dom";



export default function ProductOrderPage() {
    const firstTime = "firstTime";
    const {productId} = useParams();
    const [expandedValue, setExpandedIndex] = useState(firstTime);

    const handleClick = (index: string): void => index === expandedValue ? setExpandedIndex("") : setExpandedIndex(index);



    const formStyle = 'bg-gradient-to-br from-white to-sky-200 rounded-b-sm  shadow-xl/20 p-3';

    const sharedIconStyle = " text-2xl rounded-lg shadow-sm/20 "; 


    return <div className="mt-9 text-center flex flex-col gap-5 items-center mb-8 p-0  lg:p-9">
        <div className="  lg:bg-radial  lg:from-indigo-800 lg:to-indigo-500 lg:rounded-xl shadow-lg/0 w-[90%] lg:border-white lg:border-5   lg:h-[50rem] lg:mb-[78rem]  ">
            <h1 className="lg:text-white text-4xl lg:text-8xl font-bold lg:m-8 ">

                بنطلون جبردين قماش بيتقطع بكل سهولة

            </h1>

            <div className="bg-slate-800 text-right rtl px-8 py-5">
                <h3 className="text-3xl text-white mb-8">
                    عن المنتج
                </h3>
                <ul className="text-indigo-50 text-xl  w-[100%] list-disc  space-y-2 ">
                    <li><strong>كيميت</strong>   علامتك اللي بتفهم الأسعار الحلوة والجودة مع بعض.</li>
                    <li>منتجات مختارة بعناية علشان توصل لك بسرعة من غير وجع دماغ.</li>
                    <li>طلب سهل وسريع: تختار، تضغط، ونوصّلك لحد باب البيت.</li>
                    <li>لو حاجة ما عجبتكش، استرجاع بسيط ومفيش لف ودوران.</li>
                    <li>دعم دايم معاك — تكلّم معانا وهنحلّها فوراً.</li>
                    <li>كيميت = جودة، راحة، وأسعار بتضحكك.</li>
                </ul>
            </div>



            <img src="/src/assets/sizes-weight.jpeg" className="rounded-lg mt-[5rem] lg:mt-[2rem] mx-auto  shadow-xl/50    block"></img>
        </div>




        <video
            className="my-4 rounded shadow-lg"
            controls
            width={400}
        >
            <source src={"../../src/assets/vid.mp4"} type="video/mp4" />
            Your Browser Doesn't Support Playing the video
        </video>


        <a target="_blank" className="text-violet-100  p-1 text-xl bg-purple-800 " href="https://www.youtube.com/watch?xv=1O0yazhqaxs" > شوف الفيديو</a>


    <ProductOffers productId={productId}></ProductOffers>

        <div className="shadow-md/30" >
            <OpenClosedLabel
                valueToExpand={firstTime}
                expandedValue={expandedValue}
                onClick={() => handleClick(firstTime)}
                title="اطلب الان"
                icons={
                    {
                        hover: <GrShop className={`${sharedIconStyle} border-2 border-purple-800  bg-yellow-200 text-purple-800`} />,
                        expanded: <GrShop className={`${sharedIconStyle}  `} />,
                        idle: <TbTruckDelivery className={`${sharedIconStyle}`} />
                    }
                }
               

            >
                <FirstTimeForm formStyle={formStyle}></FirstTimeForm>
            </OpenClosedLabel>
        </div>




    </div >





}
