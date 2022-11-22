import { useState } from "react";
import Switch from "react-switch";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import useAuth from "./useAuth";

const Login = () => {
  const { setAuth } = useAuth();

  const [errorMsg, setErrMsg] = useState("");

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleRegister = (e) => {
    e.preventDefault();

    console.log(email + "|" + password);
    fetch("https://localhost:7183/api/auth/login", {
      method: "POST",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email,
        password: password,
      }),
    })
      .then((res) => res.text())
      .then((response) => {
        console.log(response);

        const token = response;
        /*
                const name =  getName(response);
                const role = getRole(response);

                setAuth({ token, name, role });*/

        localStorage.setItem("token", token);
      })
      .catch((error) => {
        setErrMsg(error);
      });
  };

  return (
    <div className="grid h-screen place-items-center pb-40 ">
      <div className="text-[18px] w-[350px] sm:w-[400px] flex flex-col border-sky-500 border-2 px-6 py-3 rounded-xl shadow-xl">
        <h1 className="text-center font-bold  mb-3 text-3xl">Log In</h1>
        <form onSubmit={handleRegister}>
          <div className="flex-col flex">
            <label className="pl-1">Email:</label>
            <input
              className=" border-2 border-sky-500 rounded-xl p-1"
              type="text"
              placeholder="Email"
              id="email"
              onChange={(e) => setEmail(e.target.value)}
            ></input>
            <br></br>
          </div>
          <div className="flex flex-col">
            <label className="pl-1">Password:</label>
            <input
              className=" border-2 border-sky-500 rounded-xl p-1"
              type="password"
              placeholder="Password"
              id="password"
              onChange={(e) => setPassword(e.target.value)}
            ></input>
            <br></br>
          </div>
          <button className="mt-3 bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white h-10 w-full">
            Sign In
          </button>
        </form>
        <div className="mt-5 mb-5 bg-sky-500 h-[2px] w-full"></div>
      <h1 className="text-center font-bold font-serif">Don't have an account yet?</h1>
      <Link to="/register" className="text-center text-sky-500 hover-sky:700">Register here</Link>
      </div>
      
    </div>
  );
};

export default Login;
