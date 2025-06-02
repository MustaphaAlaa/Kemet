import { createBrowserRouter } from "react-router-dom";
import App from "../app/layout/App";
import CustomerForm from "../Features/CustomerForm";
import ColorManagement from "../Features/Colors/ColorManagement";
import { ColorProvider } from "../Contexts/color/colorProvider";
import { SizeProvider } from "../Contexts/sizes";
import SizeManagement from "../Features/Sizes/SizeManagement";  



export const router = createBrowserRouter([
    {
        path: '/',
        element: <App></App>,
        children: [
            { path: '/createOrder', element: <CustomerForm></CustomerForm> },
            {
                path: '/m/colors', element: <ColorProvider  >
                    <ColorManagement></ColorManagement>
                </ColorProvider>
            },
            {
                path: '/m/sizes', element: <SizeProvider>
                    <SizeManagement  ></SizeManagement>
                </SizeProvider>
            },
             

        ]
    }
])