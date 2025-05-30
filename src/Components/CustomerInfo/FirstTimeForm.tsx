import { useState, type ChangeEvent, type FormEvent } from 'react';
import InputText from '../ReuseableComponents/InputText';
import type { sisi } from '../../app/layout/App';
import InputNumber from '../ReuseableComponents/InputNumber';

export default function FirstTimeForm({ formStyle }: { formStyle: string }) {

    const [value, setValue] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");



    function handleSubmit(event: FormEvent<HTMLFormElement>): void {
        event.preventDefault();
        console.log(event)

        console.log("form is submitted")
    }


    const marginY = 'my-4'
    const marginB = 'mb-2';
    const md = 'md:flex-row md:justify-between';

    const sharedStyle = `${marginY}  flex flex-col md:flex-row md:justify-between  items-center`;
    const inputStyle = 'rounded-md shadow-sm'

    return (
        <form onSubmit={handleSubmit} className={formStyle}>
            <div className={`${sharedStyle}`}>
                <label htmlFor="FirstName" className={`${marginB} mx-3`}> الاسم الاول</label>

                <InputText name="FirstName" id="FirstName" styles={`${inputStyle}`}  ></InputText>
            </div>
            <div className={`${sharedStyle}`}>
                <label htmlFor="LastName" className={`${marginB} mx-3`}> الاسم الثاني</label>
                <InputText name="LastName" id="LastName"  styles={`${inputStyle}`}  ></InputText>
            </div>
            <div className='flex flex-col @md:flex-row'>
                <div className={` ${marginY}   flex flex-row items-center justify-around `}>
                    <label htmlFor="Address" className={`${marginB}`}> المحافظة</label>
                    {/* <InputText name="Address" id="Address"   ></InputText> */}
                    <select className='bg-white p-3 rounded-xl cursor-pointer text-indigo-800'>
                        <option value="" selected>-- أختار المحافظة --</option>

                    </select>
                </div>
                {/* <div className={` ${marginY}  flex flex-col  sm:flex-col md:flex-row md:justify-around items-center  `}> */}
                <div className={` ${sharedStyle}`}>
                    <label htmlFor="Address" className={`${marginB}`}> العنوان</label>
                    <InputText name="Address" id="Address"  styles={`${inputStyle}`} ></InputText>
                </div>
            </div>
            {/* <div className={` ${marginY}  flex flex-col md:flex-row items-center  `}> */}
            <div className={` ${sharedStyle}`}>
                <label htmlFor="PhoneNumber" className={`${marginB}`}> رقم الموبايل</label>
                <InputNumber placeholder="01*********" name="phoneNumber" id="PhoneNumber" val={setPhoneNumber} maxLength={11}  styles={`${inputStyle}`}  ></InputNumber>
            </div>
        </form>
    )
}
