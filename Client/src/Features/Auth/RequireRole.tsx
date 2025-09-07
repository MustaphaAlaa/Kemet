import { useLocation, Navigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectUserRoles } from "../../../store/store";

const RequireRole = ({ allowedRoles }: { allowedRoles: string[] }) => {
    const roles = useSelector(selectUserRoles);

    const location = useLocation();

    let content;
    if (allowedRoles.some(role => roles?.includes(role))) {
        content = <>
            <Outlet />
        </>
    } else content = <Navigate to="/" state={{ from: location }} replace />

    return content;
};



export default RequireRole;
