import { useLocation, Navigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";
import { selectCurrentToken } from "../../../store/slices/authSlice";

const RequireAuth = () => {
  const token = useSelector((state) => state.auth.token);

  const location = useLocation();

  return token ? 'Token is exist' : 'No token yet';

  return token ? (
    <Outlet />
  ) : (
    <Navigate to="/login" state={{ from: location }} replace />
  );
};
export default RequireAuth;
