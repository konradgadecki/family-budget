import { useState, useEffect, createRef, FormEvent } from "react";
import {
  faCheck,
  faTimes,
  faInfoCircle,
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Link, useNavigate, useLocation } from "react-router-dom";
import { isAxiosError } from "axios";
import { signUp } from "../services/AuthService";
import { NAVIGATION } from "../utils/navigation";

const EMAIL_REGEX = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%&]).{8,24}$/;

const SignUp = () => {
  const emailRef = createRef<HTMLInputElement>();
  const errRef = createRef<HTMLInputElement>();

  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const [email, setEmail] = useState("");
  const [validEmail, setValidEmail] = useState(false);
  const [emailFocus, setEmailFocus] = useState(false);

  const [pwd, setPwd] = useState("");
  const [validPwd, setValidPwd] = useState(false);
  const [pwdFocus, setPwdFocus] = useState(false);

  const [matchPwd, setMatchPwd] = useState("");
  const [validMatch, setValidMatch] = useState(false);
  const [matchFocus, setMatchFocus] = useState(false);

  const [errorMessage, setErrorMessage] = useState("");
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    emailRef?.current?.focus();
  }, []);

  useEffect(() => {
    setValidEmail(EMAIL_REGEX.test(email));
  }, [email]);

  useEffect(() => {
    setValidPwd(PWD_REGEX.test(pwd));
    setValidMatch(pwd === matchPwd);
  }, [pwd, matchPwd]);

  useEffect(() => {
    setErrorMessage("");
  }, [email, pwd, matchPwd]);

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const emailValid = EMAIL_REGEX.test(email);
    const passwordValid = PWD_REGEX.test(pwd);
    if (!emailValid || !passwordValid) {
      setErrorMessage("Invalid Credentials");
      return;
    }

    const onSuccessSignUp = () => {
      setSuccess(true);
      setEmail("");
      setPwd("");
      setMatchPwd("");
    };

    const onErrorSignUp = (err: unknown) => {
      if (isAxiosError(err) && !err?.response) {
        setErrorMessage("No Server Response");
      } else if (isAxiosError(err) && err.response?.status === 409) {
        setErrorMessage("email Taken");
      } else {
        setErrorMessage("Registration Failed");
      }
      errRef?.current?.focus();
    };

    try {
      await signUp(email, pwd);
      onSuccessSignUp();
    } catch (err) {
      onErrorSignUp(err);
    }
  };

  const onSignInClicked = () => {
    navigate(from, { replace: true });
  };

  const renderSuccessSection = () => (
    <>
      <section className="success-section">
        <h1 className="success-title">Success!</h1>
        <div className="success-button-container">
          <button onClick={onSignInClicked}>Sign In</button>
        </div>
      </section>
    </>
  );

  const renderErrorMessage = () => (
    <>
      <p
        ref={errRef}
        className={errorMessage ? "errorMessage" : "offscreen"}
        aria-live="assertive"
      >
        {errorMessage}
      </p>
    </>
  );

  return (
    <>
      {success ? (
        renderSuccessSection()
      ) : (
        <section>
          {renderErrorMessage()}
          <h1>Sign Up</h1>
          <form onSubmit={handleSubmit}>
            <label htmlFor="email">
              Email:
              <FontAwesomeIcon
                icon={faCheck}
                className={validEmail ? "valid" : "hide"}
              />
              <FontAwesomeIcon
                icon={faTimes}
                className={validEmail || !email ? "hide" : "invalid"}
              />
            </label>
            <input
              type="text"
              id="email"
              ref={emailRef}
              autoComplete="off"
              onChange={(e) => setEmail(e.target.value)}
              value={email}
              required
              aria-invalid={validEmail ? "false" : "true"}
              aria-describedby="uidnote"
              onFocus={() => setEmailFocus(true)}
              onBlur={() => setEmailFocus(false)}
            />
            <p
              id="uidnote"
              className={
                emailFocus && email && !validEmail
                  ? "instructions"
                  : "offscreen"
              }
            >
              <FontAwesomeIcon icon={faInfoCircle} />
              4 to 24 characters.
              <br />
              Must begin with a letter.
              <br />
              Letters, numbers, underscores, hyphens allowed.
            </p>

            <label htmlFor="password">
              Password:
              <FontAwesomeIcon
                icon={faCheck}
                className={validPwd ? "valid" : "hide"}
              />
              <FontAwesomeIcon
                icon={faTimes}
                className={validPwd || !pwd ? "hide" : "invalid"}
              />
            </label>
            <input
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              value={pwd}
              required
              aria-invalid={validPwd ? "false" : "true"}
              aria-describedby="pwdnote"
              onFocus={() => setPwdFocus(true)}
              onBlur={() => setPwdFocus(false)}
            />
            <p
              id="pwdnote"
              className={pwdFocus && !validPwd ? "instructions" : "offscreen"}
            >
              <FontAwesomeIcon icon={faInfoCircle} />
              8 to 24 characters.
              <br />
              Must include uppercase and lowercase letters, a number and a
              special character.
              <br />
              Allowed special characters:{" "}
              <span aria-label="exclamation mark">!</span>{" "}
              <span aria-label="at symbol">@</span>{" "}
              <span aria-label="hashtag">#</span>{" "}
              <span aria-label="dollar sign">$</span>{" "}
              <span aria-label="percent">%</span>{" "}
              <span aria-label="ampersand">&</span>
            </p>

            <label htmlFor="confirm_pwd">
              Confirm Password:
              <FontAwesomeIcon
                icon={faCheck}
                className={validMatch && matchPwd ? "valid" : "hide"}
              />
              <FontAwesomeIcon
                icon={faTimes}
                className={validMatch || !matchPwd ? "hide" : "invalid"}
              />
            </label>
            <input
              type="password"
              id="confirm_pwd"
              onChange={(e) => setMatchPwd(e.target.value)}
              value={matchPwd}
              required
              aria-invalid={validMatch ? "false" : "true"}
              aria-describedby="confirmnote"
              onFocus={() => setMatchFocus(true)}
              onBlur={() => setMatchFocus(false)}
            />
            <p
              id="confirmnote"
              className={
                matchFocus && !validMatch ? "instructions" : "offscreen"
              }
            >
              <FontAwesomeIcon icon={faInfoCircle} />
              Must match the first password input field.
            </p>

            <button
              disabled={!validEmail || !validPwd || !validMatch ? true : false}
            >
              Sign Up
            </button>
          </form>
          <p>
            Already signed up?
            <br />
            <span className="line">
              <Link to={`/${NAVIGATION.SIGN_IN}`}>Sign In</Link>
            </span>
          </p>
        </section>
      )}
    </>
  );
};

export default SignUp;
