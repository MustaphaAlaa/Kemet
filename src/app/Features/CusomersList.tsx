import { useCallback, useEffect, useState, type JSX } from "react";
// import fetchFromUrl from "../../../APIFunctions/fetchFromUrl";
import type { Customer } from "../../Models/Customer";
import type { APIResponse } from "../../Models/APIResponse";
import axios from "axios";
import CustomerCard from "../CustomerInfo/CustomerCard";

export default function CustomerList() {
    const [response, setResponse] = useState<APIResponse<Customer[]>>();


    const deleted = (index?: number) => {


        const filteredCustomersList = response?.result?.filter((item: Customer) => {
            return item.customerId !== index;
        })


        const newResponseObj: APIResponse<Customer[]> = { ...response, result: filteredCustomersList, }
        setResponse(newResponseObj);
    }

    // useEffect(() => {
    //     handleClick();
    // } ,[])


    useEffect(() => {
        handleClick()
    }
        , []);

    const handleClick = async () => {

        const { data } = await axios.get(`https://localhost:7048/api/admin/Customer/all/`);
        setResponse(data);
    };


    const c = response?.result?.map((item: Customer) => {
        return <CustomerCard dlt={deleted} customer={item} key={item.customerId}></CustomerCard>

    });
    return <>
        <div className="flex flex-wrap flex-col md:flex-row m-3">
            {c}
        </div>
        <button className="p-4 bg-blue-100 text-4xl rounded-xl shadow-md/30" onClick={handleClick}>Get The Customers</button>
    </>
}