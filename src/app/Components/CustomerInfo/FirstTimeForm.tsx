import { useState, type ChangeEvent, type FormEvent } from 'react';
import InputText from '../ReuseableComponents/InputText';

export default function FirstTimeForm() {

    const [value, setValue] = useState("");

    function handleSubmit(event: FormEvent<HTMLFormElement>): void {
        event.preventDefault();
        console.log(event)

        console.log("form is submitted")
    }

    function handelChange(event: ChangeEvent<HTMLInputElement>): void {

        // const val = event.target.value;
        // setValue(val.trimStart());

    }


    return (
        <form onSubmit={handleSubmit} className='bg-sky-100 p-3 shadow '>
            <div className=' m-2  flex     items-center '>
                <label htmlFor="FirstName" className='w-32'> الاسم الاول</label>
                <InputText name="FirstName" id="FirstName" onChange={handelChange}  ></InputText>
            </div>
            <div className=' m-2  flex     items-center '>
                <label htmlFor="LastName" className='w-32'> الاسم الثاني</label>
                <InputText name="LastName" id="LastName" onChange={handelChange}  ></InputText>
            </div>
            <div className=' m-2  flex     items-center '>
                <label htmlFor="Address" className='w-32'> العنوان</label>
                <InputText name="Address" id="Address" onChange={handelChange}  ></InputText>
            </div>
            <div className=' m-2  flex     items-center '>
                <label htmlFor="PhoneNumber" className='w-32'> رقم الموبايل</label>
                <InputText name="PhoneNumber" id="PhoneNumber" number  ></InputText>
            </div>
        </form>
    )
}
