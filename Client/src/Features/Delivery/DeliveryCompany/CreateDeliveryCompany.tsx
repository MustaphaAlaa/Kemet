import { useState, type FormEvent } from "react";
import { InputTextValChange } from "../../../Components/ReuseableComponents/InputText";
import AddButton from "../../../Components/ReuseableComponents/AddButton";
import ApiLinks from "../../../APICalls/ApiLinks";
import { authorizeAxios } from "../../../APICalls/authorizeAxios.tsx";



export default function CreateDeliveryCompany() {
  const today = new Date();
  const yyyyMMdd = today.toISOString().split('T')[0];
  const [date, setDate] = useState<string>(yyyyMMdd);
  const [name, setName] = useState<string>();

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    await authorizeAxios.post(ApiLinks.deliveryCompany.create, { Name: name, DialingWithItFrom: date })

  }
  return (
    <form onSubmit={handleSubmit} className="flex flex-col space-y-8 justify-center items-center "   >

      <div>
        <InputTextValChange placeholder="اسم شركة الشحن" styles="rounded-md shadow-md/20 text-blue-900" setTextValue={setName}></InputTextValChange>
      </div>
      <div className=" flex flex-col md:flex-row items-center space-y-3 md:space-y-0 md:space-x-8">
        <label>بدء التعامل  من</label>
        <input lang="ar" value={date} onChange={(e) => setDate(e.target.value)} type="date" className="  bg-white p-2 rounded-md shadow-md/20"></input>
      </div>
      <AddButton label={`إضف الشركة`}></AddButton>
    </form>
  )
}
