import { createBrowserRouter } from "react-router-dom";

import * as elements from './elementsToToRouting';

export const router = createBrowserRouter([
  {
    path: "/",
    element: <elements.App></elements.App>,
    children: [
      { path: "/createOrder", element: <elements.CustomerForm></elements.CustomerForm> },

      {
        element: <elements.RequireAuth></elements.RequireAuth>,
        children: [{
          path: "/m/colors",
          element: (
            <elements.ColorProvider>
              <elements.ColorManagement></elements.ColorManagement>
            </elements.ColorProvider>
          ),
        },]
      },

      {
        path: "/m/sizes",
        element: (
          <elements.SizeProvider>
            <elements.SizeManagement></elements.SizeManagement>
          </elements.SizeProvider>
        ),
      },
      {

        path: "/productsPage",
        element: <elements.ProductsPage></elements.ProductsPage>,
      },
      {
        path: "/m/createProduct",
        element: <elements.CreateProduct></elements.CreateProduct>,
      },
      {
        path: `${elements.NavigationLinks.product.productStock}/:productId`,
        element: <elements.ProductVariantStockPage></elements.ProductVariantStockPage>,
      },
      {
        path: "/ManageCustomers",
        element: <elements.CustomerList></elements.CustomerList>,
      },
      {
        path: `${elements.NavigationLinks.product.productPrice}/:productId`,
        element: <elements.ProductVariantPricePage></elements.ProductVariantPricePage>,
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
        path: `${elements.NavigationLinks.deliveryManagement.deliveryCompany.orders}/:deliveryCompanyId`,
        element: <elements.DeliveryCompanyOrders></elements.DeliveryCompanyOrders>
      },
      {
        path: `${elements.NavigationLinks.deliveryManagement.governorate.all}`,
        element: <elements.ManageCustomerGovernorateDeliveryList></elements.ManageCustomerGovernorateDeliveryList>
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
    ],
  },
]);
