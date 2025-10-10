import { FaDollarSign } from "react-icons/fa";
import { NavigationLinks } from "../../../Navigations/NavigationLinks";
import { rolesTypes } from "../../../routes/routesRoles";
import { MdOutlineInventory2 } from "react-icons/md";
import { AiOutlineSnippets } from "react-icons/ai";



export interface ProductActionButtonConfig {
    to: string;
    button: {
        styles: Record<string, boolean>;
        content: React.ReactNode;
    };
}

export type RoleBasedProductActions = {
    [role in typeof rolesTypes[keyof typeof rolesTypes]]?: ProductActionButtonConfig[];
};

export const productActionButtonsByRole: RoleBasedProductActions = {
    [rolesTypes.ADMIN]: [
        {
            to: `${NavigationLinks.product.productPrice}/:productId`, button: {
                styles: { roundedXs: true, hover: true, primary: true }, content: <>
                    إدارة اسعار المنتج
                    <span>< FaDollarSign className="text-xl" /> </span>

                </>
            }
        },
    ],
    [rolesTypes.EMPLOYEE]: [
        {
            to: `${NavigationLinks.product.productStock}/:productId`, button: {
                styles: { roundedLg: true, hover: true, warning: true },
                content: <>
                    إدارة مخزون المنتج
                    <span>< MdOutlineInventory2 />  </span >
                </>
            }
        },
        {
            to: `${NavigationLinks.product.orders}/:productId`, button: {
                styles: { roundedLg: true, hover: true, primary: true, outline: true },
                content: <>
                    الطلبات

                    < span >
                        <AiOutlineSnippets className="text-xl" />
                    </ span>

                </>
            }
        },
    ]
}