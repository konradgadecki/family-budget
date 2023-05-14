import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import { CustomAuthContext } from "../types/apiTypes";

const useAuth = (): CustomAuthContext => {
  return useContext(AuthContext);
};

export default useAuth;
