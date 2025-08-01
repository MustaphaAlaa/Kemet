export interface OrderInfoDTO {
    customerName: string;
    governorateName: string;
    streetAddress: string;
    orderId: number;
    productId: number;
    orderStatusId: number;
    orderReceiptStatusId: number | null;
    totalPrice: number;
    governorateDeliveryCost: number;
    governorateId: number;
    quantity: number;
    createdAt: string;
}