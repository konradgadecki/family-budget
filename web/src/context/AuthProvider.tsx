import { ReactNode, createContext, useState } from "react";

type AuthProviderProps = {
  children: ReactNode;
};

export type Auth = {
  email?: string;
  pwd?: string;
  roles?: Array<number>;
  accessToken?: string;
};

export type CustomAuthContext = {
  auth: Auth;
  setAuth: React.Dispatch<React.SetStateAction<Auth>>;
};
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
