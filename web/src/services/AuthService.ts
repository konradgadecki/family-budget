import axios from "../api/axios";
const SIGN_UP_URL = "/Users/sign-up";

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
