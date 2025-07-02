import Dictionary from "../Components/dictionary";



 





const NavigationLinks = {
    color: {
        manageColors: '/m/colors'
    },
    product: {
        productStock: `/m/product/stock`,
        productPrice: '/m/product/price',
        productQuantityPrice: '/m/product/Quantity/price',
    },
    deliveryManagement: {
        manageDelivery: `/m/manageDelivery`,
        deliveryCompany: {
            create: `/m/deliveryCompany/create`,
            update: `/m/deliveryCompany/update`,
            all: `/m/deliveryCompany/list`,
            governorateDeliveryCost: `/m/deliveryCompany/delivery-cost`,
            page:   `/m/deliveryCompany`,
            governorates:   `/m/deliveryCompany/governorates`,

        },
        governorate: {
            all: `/m/governorate/list`,
            customerDeliveryCost: `/m/governorate/customer/delivery-cost`,
         
        }

    }
} 
 

export { NavigationLinks };