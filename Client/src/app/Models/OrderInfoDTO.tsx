export interface OrderInfoDTO {
    customerName: string;
    governorateName: string;
    streetAddress: string;
    orderId: number;
    productId: number;
    orderStatusId: number;
    orderReceiptStatusId: number | null;
    totalPrice: number;
    quantity: number;
    createdAt: string;
}