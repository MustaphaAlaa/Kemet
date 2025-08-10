import Dictionary from "../Components/dictionary";





const management = `/m`;



const NavigationLinks = {
    color: {
        manageColors: `${management}/colors`
    },
    product: {
        productStock: `${management}/product/stock`,
        productPrice: `${management}/product/price`,
        productQuantityPrice: `${management}/product/Quantity/price`,
        page: `${management}/product`,
        orders: `${management}/product/orders`,
    },
    deliveryManagement: {
        manageDelivery: `${management}/manageDelivery`,
        deliveryCompany: {
            create: `${management}/deliveryCompany/create`,
            update: `${management}/deliveryCompany/update`,
            all: `${management}/deliveryCompany/list`,
            governorateDeliveryCost: `${management}/deliveryCompany/delivery-cost`,
            page: `${management}/deliveryCompany`,
            governorates: `${management}/deliveryCompany/governorates`,
            orders: `${management}/deliveryCompany/orders`,

        },
        governorate: {
            all: `${management}/governorate/list`,
            customerDeliveryCost: `${management}/governorate/customer/delivery-cost`,

        }

    },
    orders: {
        orderDetails: `${management}/order/details`
    }
}


export { NavigationLinks };