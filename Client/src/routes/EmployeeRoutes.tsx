import * as elements from './elementsToRouting';

export const EmployeeRoutes = [{
    path: `${elements.NavigationLinks.product.productStock}/:productId`,
    element: <elements.ProductVariantStockPage></elements.ProductVariantStockPage>,
},


{
    path: `${elements.NavigationLinks.product.page}/:productId`,
    element: <elements.ProductPage></elements.ProductPage>
},


{
    path: `${elements.NavigationLinks.product.orders}/:productId`,
    element: <elements.OrderStatusPage></elements.OrderStatusPage>
},
{
    path: `${elements.NavigationLinks.orders.orderDetails}/:orderId`,
    element: <elements.OrderDetailsPage></elements.OrderDetailsPage>
},

]