import type { ChangeEvent, FormEvent } from 'react';
import InputText from '../ReuseableComponents/InputText';

export default function NotFirstTimeForm() {
  function handleSubmit(event: FormEvent<HTMLFormElement>): void {
        event.preventDefault();
        console.log(event)

        console.log("form is submitted")
    }

    function handelChange(event: ChangeEvent<HTMLInputElement>): void {
        if (event.target.value.length == 11)
            console.log(event.target.value)
    }



    return (
        <form onSubmit={handleSubmit} className='bg-sky-100 p-3 shadow '>
            <div className='   flex     items-center '>
                <label htmlFor="phoneNumber" className='w-32'> رقم الموبايل</label>
                {/* <input type="text" name="phoneNumber" id="phoneNumber" onChange={handelChange} maxLength={11} className='shadow w-2xs  bg-white focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500' /> */}
                <InputText name="phoneNumber" id="phoneNumber" onChange={handelChange} maxLength={11} styles='bg-red-500 text-sky-600  ' ></InputText>
            </div>
        </form>
    )
}
