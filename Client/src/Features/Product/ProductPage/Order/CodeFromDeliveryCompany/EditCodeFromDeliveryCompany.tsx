import { useState, type FormEvent } from "react";
import ApiLinks from "../../../../../APICalls/ApiLinks";
import type { APIResponse } from "../../../../../app/Models/APIResponse";
import type { OrderReadDTO } from "../../../../../app/Models/OrderReadDTO";
import { authorizeAxios } from "../../../../../APICalls/authorizeAxios.tsx";

export default function EditCodeFromDeliveryCompany({ codeFromDeliveryCompany,
    orderId, updateMode,
    setCodeFromDeliveryCompany
}: {
    orderId: number,
    codeFromDeliveryCompany: string | null,
    updateMode: React.Dispatch<React.SetStateAction<boolean>>,
    setCodeFromDeliveryCompany: React.Dispatch<React.SetStateAction<string | null>>
}) {

    const handleSubmit = async (event: FormEvent) => {
        event.preventDefault();

        try {
            const { data }: { data: APIResponse<OrderReadDTO> } = await authorizeAxios.put(
                ApiLinks.orders.updateOrderDeliveryCompanyCode(orderId),
                { codeFromDeliveryCompany: textValue }
            );

            if (data.isSuccess) {
                setCodeFromDeliveryCompany(data.result?.codeFromDeliveryCompany ?? null);
                updateMode(false);
            }

        } catch (error) {
            console.error("Failed to update code from delivery company:", error);
        }


    }

    const handleChange = (event: FormEvent) => {
        let value = event.target.value;
        setTextValue(value);
    }

    const [textValue, setTextValue] = useState(codeFromDeliveryCompany ?? "");

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={textValue}
                onChange={handleChange}
                name="codeFromDeliveryCompany"
                className="bg-gray-100 border-2 shadow-xl/20 border-gray-300 rounded-sm text-center text-gray-700 font-bold"
                autoFocus
            />
        </form>
    )
}