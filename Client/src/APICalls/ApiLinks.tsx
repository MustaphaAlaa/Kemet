import ApiDomain from "../app/Models/ApiDomain";



const apiDomain = `${ApiDomain}/api`


const ApiLinks = {
  category: {
    getAll: `${apiDomain}/category/index`,
  },
  color: {
    getAll: `${apiDomain}/color/index`,
    getColor: `${apiDomain}/color`,
    create: `${apiDomain}/a/color`,
    delete: `${apiDomain}/a/color`,
    update: `${apiDomain}/a/color`,
  },
  size: {
    getAll: `${apiDomain}/size/index`,
    create: `${apiDomain}/a/size`,
    delete: `${apiDomain}/a/size`,
    update: `${apiDomain}/a/size`,
  },
  product: {
    create: `${apiDomain}/a/product`,
    get: `${apiDomain}/product`
  },
  productVariant: {
    details: `${apiDomain}/productVariant/details`,
    endpoint: `${apiDomain}/productVariant/details`,
    stock: `${apiDomain}/productVariant/stock`,
  },
  price: {
    get: `${apiDomain}/a/prices/product/range`,
    create: `${apiDomain}/a/prices/price/range`,
    updateNote: `${apiDomain}/a/prices/price/range/note`
  },
  productQuantityPrice: {
    quantitiesPrices: `${apiDomain}/productQuantityPrice`,
    createQuantitiesPrices: `${apiDomain}/a/productQuantityPrice`
  },
  deliveryCompany: {
    getAll: `${apiDomain}/a/DeliveryCompany/all`,
    getAllForGovernorate: (governorateId: number) => `${apiDomain}/a/DeliveryCompany/activeGovernorate/${governorateId}`,
    getOrders: (governorateId: number, page: number, pageSize: number) => `${apiDomain}/e/orders/DeliveryCompany/${governorateId}?pageNumber=${page}&pageSize=${pageSize}`,

    get: (id: number) => `${apiDomain}/a/DeliveryCompany/${id}`,
    create: `${apiDomain}/a/DeliveryCompany`,
    update: `${apiDomain}/a/DeliveryCompany`,
    delete: `${apiDomain}/a/DeliveryCompany`,
    activeGovernorates: (id: number) => `${apiDomain}/a/DeliveryCompany/${id}/activeGovernorates`,
    updateGovernorateCost: (id: number) => `${apiDomain}/a/DeliveryCompany/${id}/Governorate`,
  },
  governorateDelivery: {
    admin: {
      getAll: `${apiDomain}/a/governorateDelivery/all`,
      update: `${apiDomain}/a/governorateDelivery`,
    },
    customer: {
      getAll: `${apiDomain}/governorateDelivery/all`
    }

  },
  orders: {
    helper: {
      orderStatuses: `${apiDomain}/e/orders/helper/statuses`,
      orderReceiptStatuses: `${apiDomain}/e/orders/helper/ReceiptStatuses`,
    },
    export: `${apiDomain}/a/orders/excel`,
    ordersForStatus: (productId: number, orderStatusId: number, pageNumber = 1, pageSize = 2) => `${apiDomain}/e/orders/product/${productId}/status/${orderStatusId}?pageNumber=${pageNumber}&pageSize=${pageSize}`,
    updateOrderDeliveryCompany: (orderId: number, deliveryCompanyId: number, governorateId: number) => `${apiDomain}/e/orders/DeliveryCompany/${orderId}/${deliveryCompanyId}/${governorateId}`,
    updateOrderDeliveryCompanyCode: (orderId: number) => `${apiDomain}/e/orders/DeliveryCompanyCode/${orderId}`,
    updateOrderNote: (orderId: number) => `${apiDomain}/e/orders/Note/${orderId}`,
    customerInfo: (orderId: number) => `${apiDomain}/e/orders/customer/${orderId}`,
    OrderItems: (orderId: number) => `${apiDomain}/e/orders/orderItems/${orderId}`,
    SearchOrder: (deliveryCompanyCode: string) => `${apiDomain}/e/orders/search/order?deliveryCompanyCode=${deliveryCompanyCode}`,
    updateOrderStatus: `${apiDomain}/e/orders/UpdateStatus`,
    updateOrderReceiptStatus: `${apiDomain}/e/orders/UpdateReceiptStatus`,

  }
};

export default ApiLinks;
