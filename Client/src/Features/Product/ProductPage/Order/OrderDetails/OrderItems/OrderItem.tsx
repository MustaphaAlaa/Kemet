import type { OrderItemWithProductVariantData } from "../../../../../../app/Models/OrderItemWithProductVariantData";


export default function OrderItem({ orderItem }: { orderItem: OrderItemWithProductVariantData }) {
    return (
        <div className="flex flex-row">
            <div>
                or: {orderItem.quantity}
            </div>
        </div>
    )
}
