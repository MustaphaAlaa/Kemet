export interface OrderReadDTO {
    orderId?: number | null;
    customerId?: string | null;
    orderReceiptStatusId?: number | null;
    orderStatusId?: number | null;
    note?: string | null;
    codeFromDeliveryCompany?: string | null;
    orderDate?: Date | null;
    updatedAt?: Date | null;
}