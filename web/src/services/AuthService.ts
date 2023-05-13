import { AxiosResponse } from "axios";
import axios from "../api/axios";
import { LOGIN_URL, SIGN_UP_URL } from "../utils/navigation";

export const signIn = async (email: string, pwd: string) => {
  try {
    const response: AxiosResponse = await axios.post(
      LOGIN_URL,
      JSON.stringify({ email: email, password: pwd }),
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );

    return response;
  } catch (err) {
    throw err;
  }
};

export const signUp = async (email: string, pwd: string) => {
  try {
    await axios.post(
      SIGN_UP_URL,
      JSON.stringify({ email, password: pwd, role: "user" }),
      {
        headers: { "Content-Type": "application/json" },
        withCredentials: true,
      }
    );

    return;
  } catch (err) {
    throw err;
  }
};
