
import { useState } from 'react';
import Switch from "react-switch";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import { useContext } from 'react';
import useAuth from './useAuth';

const Login = () => {

    const { setAuth } = useAuth();


    

    

    const [errorMsg, setErrMsg] = useState("");

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const handleRegister = (e) => {
        e.preventDefault();

        console.log(email + "|" + password);
        fetch("https://localhost:7183/api/auth/login",
            {
                method: "POST",
                headers: {
                    "Access-Control-Allow-Origin": "*",
                    "Content-Type": "application/json"                },
                body: JSON.stringify(
                    {
                        email: email,
                        password: password
                    }
                )
            }).
            then((res) => res.text()).
            then((response) => {

                console.log(response);

                


                const token = response;
                /*
                const name =  getName(response);
                const role = getRole(response);

                setAuth({ token, name, role });*/

                localStorage.setItem("token", token);

                

            
                
            }).catch(error => {

                setErrMsg(error);
            });
    };



    return (

        <div>

            
            <div className="text-[20px] ml-10 mt-10">
                <form onSubmit={handleRegister}>
                    <label>Email:</label>
                    <input className=" mb-2 ml-[50px] border-2" type="text" id="email" onChange={(e) => setEmail(e.target.value)}></input><br></br>
                    <label>Password:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="password" id="password" onChange={(e) => setPassword(e.target.value)} ></input><br></br>

                    <button className=" mb-2   border-2">Sign In</button>
                </form>
            </div>
        </div>
        
        )

}

export default Login;