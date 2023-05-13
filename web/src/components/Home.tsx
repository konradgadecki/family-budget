import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import Budgets from "./Budgets/Budgets";

const Home = () => {
  const { setAuth } = useContext(AuthContext);
  const navigate = useNavigate();

  const logout = async () => {
    setAuth({});
    navigate("/");
  };

  return (
    <>
      <Budgets />
      <br />
      <br />
      <button onClick={logout}>Sign Out</button>
    </>
  );
};

export default Home;
