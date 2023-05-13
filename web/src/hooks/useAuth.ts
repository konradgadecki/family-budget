import { useContext } from "react";
import AuthContext, { CustomAuthContext } from "../context/AuthProvider";

const useAuth = (): CustomAuthContext => {
  return useContext(AuthContext);
};

export default useAuth;
