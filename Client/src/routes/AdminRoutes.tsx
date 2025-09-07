
import * as elements from './elementsToRouting';

export const AdminRoutes = [

    {
        path: "/m/colors",
        element: (
            <elements.ColorProvider>
                <elements.ColorManagement></elements.ColorManagement>
            </elements.ColorProvider>
        ),
    },
    {
        path: "/m/sizes",
        element: (
            <elements.SizeProvider>
                <elements.SizeManagement></elements.SizeManagement>
            </elements.SizeProvider>
        ),
    },
    // ## Customers
    {
        path: "/ManageCustomers",
        element: <elements.CustomerList></elements.CustomerList>,
    },

    // ## Product
    {
        path: "/m/createProduct",
        element: <elements.CreateProduct></elements.CreateProduct>,
    },
    {
        path: `${elements.NavigationLinks.product.productPrice}/:productId/editMode`,
        element: <elements.ProductPriceRangeEditPage></elements.ProductPriceRangeEditPage>,
    },


    {
        path: `${elements.NavigationLinks.product.productQuantityPrice}/:productId`,
        element: <elements.CreateProductQuantityPrice></elements.CreateProductQuantityPrice>,
    },

    {
        path: `${elements.NavigationLinks.product.productPrice}/:productId`,
        element: <elements.ProductVariantPricePage></elements.ProductVariantPricePage>,
    },

    //## Delivery Routes 
    {
        path: `${elements.NavigationLinks.deliveryManagement.manageDelivery}`,
        element: <elements.ManageDelivery></elements.ManageDelivery>,
    },


    {
        path: `${elements.NavigationLinks.deliveryManagement.deliveryCompany.page}/:id`,
        element: <elements.DeliveryCompanyPage></elements.DeliveryCompanyPage>
    },
    {
        path: `${elements.NavigationLinks.deliveryManagement.deliveryCompany.governorates}/:id`,
        element: <elements.DeliveryCompanyGovernorateList></elements.DeliveryCompanyGovernorateList>
    },

    {
        path: `${elements.NavigationLinks.deliveryManagement.governorate.all}`,
        element: <elements.ManageCustomerGovernorateDeliveryList></elements.ManageCustomerGovernorateDeliveryList>
    },


    {
        path: `${elements.NavigationLinks.deliveryManagement.deliveryCompany.orders}/:deliveryCompanyId`,
        element: <elements.DeliveryCompanyOrders></elements.DeliveryCompanyOrders>
    },

]