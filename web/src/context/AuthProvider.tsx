import { ReactNode, createContext, useState } from "react";

type AuthProviderProps = {
  children: ReactNode;
};

const AuthContext = createContext({});

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [auth, setAuth] = useState({});
  console.log(auth);
  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
