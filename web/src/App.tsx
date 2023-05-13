import SignIn from "./components/SignIn";
import SignUp from "./components/SignUp";
import Layout from "./components/Layout";

import { Routes, Route } from "react-router-dom";
import Unauthorized from "./components/Unauthorized";
import { ROLES } from "./utils/constants";
import RequireAuth from "./components/RequireAuth";
import Home from "./components/Home";
import { NAVIGATION } from "./utils/navigation";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route path={NAVIGATION.SIGN_IN} element={<SignIn />} />
        <Route path={NAVIGATION.UNAUTHORIZED} element={<Unauthorized />} />
        <Route path={NAVIGATION.SIGN_UP} element={<SignUp />} />
        <Route element={<RequireAuth allowedRoles={[ROLES.USER]} />}>
          <Route path="/" element={<Home />} />
        </Route>
      </Route>
    </Routes>
  );
}

export default App;
