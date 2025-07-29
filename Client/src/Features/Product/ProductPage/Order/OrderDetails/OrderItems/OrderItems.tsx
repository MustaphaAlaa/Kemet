import ApiLinks from "../../../../../../APICalls/ApiLinks";
import GetData from "../../../../../../APICalls/GetData";
import type { OrderItemWithProductVariantData } from "../../../../../../app/Models/OrderItemWithProductVariantData";
import Table, { type TableConfig } from "../../../../../../Components/ReuseableComponents/Table";


export default function OrderItems({ orderId }: { orderId: number }) {
    const { data } = GetData<OrderItemWithProductVariantData[]>(`${ApiLinks.orders.OrderItems(orderId)}`);
    console.log(data);
    // const orderItems = data?.map(item => <OrderItem orderItem={item} key={item.orderItemId}></OrderItem>)
    const config: TableConfig[] = [

        {
            label: 'اللون',
            render: (orderItem: OrderItemWithProductVariantData) => <div className="p-5 mx-auto rounded-full w-[1rem]" style={{ background: orderItem.color }}></div>
        },
        {
            label: 'الكمية',
            render: (orderItem: OrderItemWithProductVariantData) => <div className="p-3 "  >{orderItem.quantity}</div>
        },
        {
            label: 'المقاس',
            render: (orderItem: OrderItemWithProductVariantData) => <div className="p-3 "  >{orderItem.size}</div>
        },
        {
            label: 'سعر الوحدة',
            render: (orderItem: OrderItemWithProductVariantData) =>  <div className="p-3  space-x-2 font-bold "  ><span>{orderItem.unitPrice}</span> <span>ج.م</span></div>
        },
        {
            label: 'الاجمالى',
            render: (orderItem: OrderItemWithProductVariantData) => <div className="p-3  space-x-2 font-bold "  ><span>{orderItem.totalPrice}</span> <span>ج.م</span></div>
        },

    ]
    const keyFn = () => { }
    return (
        <Table data={data} config={config} keyFn={keyFn}  >

        </Table>
    )
}
