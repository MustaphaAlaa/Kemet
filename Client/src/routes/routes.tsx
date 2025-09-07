import { createBrowserRouter } from "react-router-dom";

import { AdminRoutes, EmployeeRoutes, rolesTypes } from "./routesRoles";
import * as elements from './elementsToRouting';

export const router = createBrowserRouter([

  {

    path: "/",
    element: <elements.App></elements.App>,
    children: [
      //public
      { path: "/createOrder", element: <elements.CustomerForm></elements.CustomerForm> },

      {
        element: <elements.RequireRole allowedRoles={[rolesTypes.ADMIN]} />,
        children: AdminRoutes
      },

      {
        element: <elements.RequireRole allowedRoles={[rolesTypes.ADMIN, rolesTypes.EMPLOYEE]} />,
        children: EmployeeRoutes
      },

      //public
      {
        path: "/productsPage",
        element: <elements.ProductsPage></elements.ProductsPage>,
      },
    ],
  },
]);
