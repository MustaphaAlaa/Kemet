import { useLocation, Navigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../../../store/store";

const RequireAuth = () => {
  const token = useSelector(selectCurrentToken);

  const location = useLocation();

  let content = <Outlet></Outlet>
  content = token ? content : <Navigate to="/login" state={{ from: location }} replace />;

  return content;
};
export default RequireAuth;
