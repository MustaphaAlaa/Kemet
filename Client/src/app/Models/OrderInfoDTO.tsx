export interface OrderInfoDTO {
    customerName: string;
    governorateName: string;
    streetAddress: string;
    codeFromDeliveryCompany: string | null;
    orderId: number;    
    productId: number;
    orderStatusId: number;
    orderReceiptStatusId: number | null;
    totalPrice: number;
    governorateDeliveryCost: number | null;
    governorateDeliveryCompanyCost: number | null;
    deliveryCompanyId: number | null;
    governorateId: number;
    quantity: number;
    createdAt: string;
}