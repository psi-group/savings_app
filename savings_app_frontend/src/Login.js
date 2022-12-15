import { useState } from "react";
import Switch from "react-switch";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import useAuth from "./useAuth";

const Login = ({ setSnackOn, setSnackMessage }) => {

    const navigate = useNavigate();
    const { setAuth } = useAuth();

    const [errorMsg, setErrMsg] = useState("");

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [loadingAuth, setLoadingAuth] = useState(false);

    const handleRegister = (e) => {
        setLoadingAuth(true);
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
            .then(async (res) => {
                
                if (res.ok) {
                    return await res.text();
                    //return text;
                }
                const msg = await res.json();
                console.log(msg);
                return Promise.reject(msg);

            })
            .then((response) => {
                console.log(response);

                //if(response.stat)

                const token = response;
                /*
                        const name =  getName(response);
                        const role = getRole(response);
        
                        setAuth({ token, name, role });*/

                localStorage.setItem("token", token);
                setLoadingAuth(false);
                setSnackMessage("Logged in successfully\n");
                setSnackOn(true);
                navigate("/");
            })
            .catch((error) => {
                setLoadingAuth(false);
                console.log("errroas ");
                console.log(error);
                console.log(error.error);
                setErrMsg(error.title);

            });
    };

    return (
        <div className="grid h-screen place-items-center pb-40 ">
            <div className="text-[18px] w-[350px] sm:w-[400px] flex flex-col border-sky-500 border-2 px-6 py-3 rounded-xl shadow-xl">
                <p className="text-red-500">{errorMsg}</p>
                <h1 className="text-center font-bold  mb-3 text-3xl">Log In</h1>
                <form onSubmit={handleRegister} className="[&>div>input]:border-[1px]">
                    <div className="flex-col flex">
                        <label className="pl-1">Email:</label>
                        <input
                            className=" focus:border-2  outline-none border-sky-500 rounded-xl p-1"
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
                            className="outline-none focus:border-2 border-sky-500 rounded-xl p-1"
                            type="password"
                            placeholder="Password"
                            id="password"
                            onChange={(e) => setPassword(e.target.value)}
                        ></input>
                        <br></br>
                    </div>
                    <button className="mt-3 bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white h-10 w-full"
                        disabled={loadingAuth}
                        
                    >
                        <>
                            
                            {loadingAuth && <div>
                                <i
                                    className="fa fa-refresh fa-spin"

                                />
                            </div>}
                            Sign in
                            
                            
                        </>
                        
                    </button>
                    
                </form>
                <div className="mt-3 mb-3 bg-sky-500 h-[2px] w-full"></div>
                <h1 className="text-center font-bold">Don't have an account yet?</h1>
                <Link to="/register" className="text-center text-sky-500 hover-sky:700">Register here</Link>
            </div>

        </div>
    );
};

export default Login;
