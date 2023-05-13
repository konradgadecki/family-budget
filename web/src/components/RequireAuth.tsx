import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";
import { NAVIGATION } from "../utils/navigation";

type RequireAuthProps = {
  allowedRoles: Array<number>;
};

const RequireAuth = ({ allowedRoles }: RequireAuthProps) => {
  const { auth } = useAuth();
  const location = useLocation();

  return auth?.roles?.find((role) => allowedRoles?.includes(role)) ? (
    <Outlet />
  ) : auth?.email ? (
    <Navigate
      to={`/${NAVIGATION.UNAUTHORIZED}`}
      state={{ from: location }}
      replace
    />
  ) : (
    <Navigate
      to={`/${NAVIGATION.SIGN_IN}`}
      state={{ from: location }}
      replace
    />
  );
};

export default RequireAuth;
