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
      <div className="sign-out">
        <button onClick={logout}>Sign Out</button>
      </div>
      <Budgets />
    </>
  );
};

export default Home;
