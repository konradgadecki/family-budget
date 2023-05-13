import SignUp from "./components/SignUp";
import Layout from "./components/Layout";

import { Routes, Route } from "react-router-dom";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        {/* <Route path="sign-up" element={<SignUp />} /> */}
        <Route path="/" element={<SignUp />} />
      </Route>
    </Routes>
  );
}

export default App;
