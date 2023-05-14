import { createContext, useState } from "react";
import { Auth, AuthProviderProps, CustomAuthContext } from "../types/apiTypes";

const AuthContext = createContext<CustomAuthContext>({} as CustomAuthContext);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [auth, setAuth] = useState<Auth>({});

  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
