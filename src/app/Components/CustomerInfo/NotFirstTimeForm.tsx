import { useState, type FormEvent } from 'react';
import InputText from '../ReuseableComponents/InputText';
import type { APIResponse } from '../../Models/APIResponse';
import fetchFromUrl from '../../../APIFunctions/fetchFromUrl';
import InputNumber from '../ReuseableComponents/InputNumber';





let previousNumber = '';

export default function NotFirstTimeForm({formStyle}:{formStyle:string}) {

    const [response, setResponse] = useState<APIResponse<boolean> | null>();
    const [phoneNumber, setPhoneNumber] = useState<string>("");



    function handleSubmit(event: FormEvent<HTMLFormElement>): void {
        event.preventDefault();
        console.log(event)

        console.log("form is submitted" + phoneNumber)


        if (phoneNumber.length == 11) {
            fetchFromUrl(`https://localhost:7048/api/Customer/Exist/${phoneNumber}`)
                .then(d => setResponse(d));
        }

    }


    console.log(phoneNumber)

    if(phoneNumber !== previousNumber && response !== null)
            setResponse(null);

    previousNumber = phoneNumber;

    return <>

            <form method='get' onSubmit={handleSubmit} className={formStyle}>
                <div className='   flex flex-col sm:flex-row justify-between gap-4    items-center m-1 '>
                    <label htmlFor="phoneNumber" className='text-xl'> رقم الموبايل</label>
                    <InputNumber  placeholder='XXX-XXX-XXX-XX' val={setPhoneNumber} name="phoneNumber" id="phoneNumber"   maxLength={11} styles='bg-red-500 text-sky-600  ' ></InputNumber>
                </div>
                {response &&

                    <div className='flex flex-col shadow-lg/30 m-3 rounded-md'>
                        {response.result ?
                            <p className='border shadow  bg-green-100 text-green-800 p-3 text-xl rounded-md'> العميل موجود فعلًا </p>
                            :
                            <p className='border shadow  bg-red-100 text-red-500 p-3'>  العميل مش موجود اتاكد من الرقم او دخل بيانات جديدة   </p>
                        }
                    </div> 
                }

            </form>
            {/* <p className=' bg-purple-100 sm-uppercase'>Lorem ipsum dolor sit amet consectetur adipisicing elit. Eligendi minima saepe distinctio qui natus enim sit! Pariatur voluptate, a ipsum expedita quia ea ipsam praesentium iusto, ut repellat sit perspiciatis.</p> */}
           
          

        </>
     
}
