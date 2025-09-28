// useRoles.ts
import { useSelector } from "react-redux";
import { type rootState } from "../../store/store";
import { rolesTypes } from "../routes/routesRoles";


const selectRoles = (state: rootState) => state.auth.role;



const selectIsAdmin = (state: rootState) => selectRoles(state)?.includes(rolesTypes.ADMIN);
const selectIsEmployee = (state: rootState) => selectRoles(state)?.includes(rolesTypes.EMPLOYEE);


export const useRoles = () => {
    const isAdmin = useSelector(selectIsAdmin);
    const isEmployee = useSelector(selectIsEmployee);

    return { isAdmin, isEmployee };
};
