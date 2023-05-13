import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../context/AuthProvider";
import Budgets from "./Budgets";

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
      <button onClick={logout}>Sign Out</button>
      {/* <div className="flexGrow"></div> */}
    </>
  );
};

export default Home;
