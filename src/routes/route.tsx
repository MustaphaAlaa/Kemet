import { createBrowserRouter } from "react-router-dom";
import App from "../app/layout/App";
import CustomerForm from "../Features/CustomerForm";
import ColorManagement from "../Features/Colors/ColorManagement";
import { ColorProvider } from "../Contexts/color/colorProvider";
import { SizeProvider } from "../Contexts/size/sizeProvider";
import SizeManagement from "../Features/Sizes/SizeManagement";
import ProductPage from "../Features/Product/ProductsPage";
import ProductVariantStock from "../Features/Product/ProductManagment/Stock/ProductVariantStock";
import CustomerList from "../Features/CustomersList";
import CreateProduct from "../Features/Product/CreateProduct/CreateProduct";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App></App>,
    children: [
      { path: "/createOrder", element: <CustomerForm></CustomerForm> },
      {
        path: "/m/colors",
        element: (
          <ColorProvider>
            <ColorManagement></ColorManagement>
          </ColorProvider>
        ),
      },
      {
        path: "/m/sizes",
        element: (
          <SizeProvider>
            <SizeManagement></SizeManagement>
          </SizeProvider>
        ),
      },
      {
        // path: '/m/product', element: <CreateProduct  ></CreateProduct>
        path: "/productsPage",
        element: <ProductPage></ProductPage>,
      },
      {
        path: "/m/createProduct",
        element: <CreateProduct></CreateProduct>,
      },
      {
        path: "/m/ProductStock/:productId",
        element: <ProductVariantStock></ProductVariantStock>,
      },
      {
        path: "/ManageCustomers",
        element: <CustomerList></CustomerList>,
      },
    ],
  },
]);
