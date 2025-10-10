export default function ShowCodeFromDeliveryCompany({ codeFromDeliveryCompany }: { codeFromDeliveryCompany: string | null }) {
    return (
        <div className="bg-gray-200  text-gray-800 font-bold rounded-xl px-4 py-1.5" >
            {codeFromDeliveryCompany ?? "لا يوجد كود شحن للشركة"}
        </div>
    )
}
