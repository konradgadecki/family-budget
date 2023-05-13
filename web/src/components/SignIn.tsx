import { useState, useEffect, createRef, FormEvent } from "react";
import useAuth from "../hooks/useAuth";
import { Link, useNavigate, useLocation } from "react-router-dom";

import { signIn } from "../services/AuthService";
import { isAxiosError } from "axios";

const SignIn = () => {
  const { setAuth } = useAuth();

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const emailRef = createRef<HTMLInputElement>();
  const errRef = createRef<HTMLInputElement>();

  const [email, setEmail] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");

  useEffect(() => {
    emailRef?.current?.focus();
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [email, pwd]);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    try {
      const response = await signIn(email, pwd);
      const accessToken = response?.data?.accessToken;
      // const roles = ["admin", "user"];

      debugger;
      setAuth({ email, pwd, accessToken });
      setEmail("");
      setPwd("");

      navigate(from, { replace: true });
    } catch (err) {
      if (isAxiosError(err) && !err?.response) {
        setErrMsg("No Server Response");
      } else if (isAxiosError(err) && err.response?.status === 400) {
        setErrMsg("Missing Username or Password");
      } else if (isAxiosError(err) && err.response?.status === 401) {
        setErrMsg("Unauthorized");
      } else {
        setErrMsg("Login Failed");
      }
      errRef?.current?.focus();
    }
  };

  return (
    <section>
      <p
        ref={errRef}
        className={errMsg ? "errmsg" : "offscreen"}
        aria-live="assertive"
      >
        {errMsg}
      </p>
      <h1>Sign In</h1>
      <form onSubmit={handleSubmit}>
        <label htmlFor="username">Username:</label>
        <input
          type="text"
          id="username"
          ref={emailRef}
          autoComplete="off"
          onChange={(e) => setEmail(e.target.value)}
          value={email}
          required
        />

        <label htmlFor="password">Password:</label>
        <input
          type="password"
          id="password"
          onChange={(e) => setPwd(e.target.value)}
          value={pwd}
          required
        />
        <button>Sign In</button>
      </form>
      <p>
        Need an Account?
        <br />
        <span className="line">
          <Link to="/sign-up">Sign Up</Link>
        </span>
      </p>
    </section>
  );
};

export default SignIn;
