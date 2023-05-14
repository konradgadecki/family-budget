import { ReactNode, Dispatch, SetStateAction } from "react";

export type RequireAuthProps = {
  allowedRoles: Array<number>;
};

export type AuthProviderProps = {
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
  setAuth: Dispatch<SetStateAction<Auth>>;
};
