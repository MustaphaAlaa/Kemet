import { NavLink } from "react-router-dom";
import Button from "../../../Components/ReuseableComponents/Button";
import type { Product } from "../../../app/Models/Product/Product";

import { useRoles } from "../../../hooks/useRoles";
import { rolesTypes } from "../../../routes/routesRoles";
import { productActionButtonsByRole } from "./ProductActionButtonsConfig";





export default function ProductActionButton({ product }: { product: Product }) {
    const { isAdmin, isEmployee } = useRoles();

    const isAuthorized = (role: string): boolean => {
        if (isAdmin) {
            return true
        }
        if (role == rolesTypes.EMPLOYEE && isEmployee) return true
        else return false;
    }
 

    const content = Object.entries(productActionButtonsByRole)
        .filter(([role,]) => isAuthorized(role))
        .flatMap(([, navs]) => navs)
        .map(nav => {
            if (nav == undefined) return;
            const productId = product.productId.toString();
            return <NavLink key={nav.to.replace(':productId', productId)} to={nav.to} state={{ product }}>
                <Button styles={
                    'shadow-md/20 flex flex-row gap-3 items-center hover:font-bold'
                }
                    {...nav.button.styles} >
                    {nav.button.content}
                </Button>
            </NavLink>
        }
        );


    return <>
        <div className="flex flex-col md:flex-row justify-center items-center">
            {...content}
        </div>
    </>
}
