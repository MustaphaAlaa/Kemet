import axios from "axios";
import type { Customer } from "../../app/Models/Customer";
import Button from "../ReuseableComponents/Button";
import { useState } from "react";
import type { APIResponse } from "../../app/Models/APIResponse";

export default function CustomerCard({ customer, dlt }: { customer: Customer, dlt: (index?: number) => void }) {
    // const [response, setResponse] = useState<APIResponse<Customer | undefined | null>>();
    // let response: APIResponse<Customer | undefined | null>;

    let count = 0;
    const handleClick = async (phoneNumber: string) => {
        // let response: APIResponse<Customer | undefined | null>;

        const { data } = await axios.delete(`https://localhost:7048/api/a/Customer/${phoneNumber}`);
            // .then(d => {
            //     console.log("Here Handle Click for Delete");
            //     console.log(d.data);
            //     // response = d.data;
            //         return d.data;       
            // })

        count++;
        console.log(count)
        // setResponse(data);
        console.log("Here Handle Click for Delete");
        console.log(data);
        if (data?.result === null)
            dlt(customer.customerId)
    }
    // console.log(count)


    return <div className=" bg-sky-300 shadow-xl/20  m-2 sm:m-1 md:mx-2 md:my-2  flex flex-col rounded-xl">

        <div className="m-2 p-9 flex flex-col  rounded-xl bg-white ">
            <h3 className="text-xl mb-5">  الاسم :
                <span className="text-blue-800">
                    {` ${customer.firstName} ${customer.lastName}`}
                </span>

            </h3>
            <h4 dir="rtl"> رقم الموبايل:  {`${customer.phoneNumber} `}  </h4>
        </div>
        <div className="flex flex-col sm:flex-row justify-center">
            <Button warning roundedMd hover className="hover:shadow-md/30 justify-center align-center" onClick={() => handleClick(customer.phoneNumber)}>  تعديل بيانات العميل</Button>
            <Button danger roundedMd hover className="hover:shadow-md/  30 justify-center align-center" onClick={() => handleClick(customer.phoneNumber)}>  حذف العميل</Button>
        </div>
    </div>
} 