
import { useState } from 'react';

const Register = () => {

    const [errorMsg, setErrMsg] = useState("");

    const handleRegister = () => {
        fetch("https://localhost:7183/api/auth/register",
            {
                method: "POST",
                headers: { 'Content-Type': 'application/json', "Access-Control-Allow-Origin": "*" },
                body: JSON.stringify({
                    Name: document.getElementById("username").value,
                    Email: document.getElementById("email").value,
                    Password: document.getElementById("password").value
                })
            }).
            then(async response => {

                const isJson = response.headers.get('content-type')?.includes('application/json');

                console.log(isJson);



                /*
                 * const data = isJson ? await response.json() : null;
                */
                console.log(response);
                if (!response.ok) {

                    setErrMsg("could not register");
                    // get error message from body or default to response status
                    
                    
                }
                else {
                    setErrMsg("");
                }
            }).catch(error => {

                setErrMsg(error);
            });
    };
            
    

    

    return (

        <div>
            <div className="text-[20px] ml-10 mt-10">
                <label>Username:</label>
                <input className=" mb-2  ml-[10px] border-2" type="text" id="username"></input><br></br>
                <label>Email:</label>
                <input className=" mb-2 ml-[50px] border-2" type="text" id="email"></input><br></br>
                <label>Password:</label>
                <input className=" mb-2 ml-[10px] border-2" type="password" id="password" ></input><br></br>
                <input className=" mb-2   border-2" type="submit" onClick={handleRegister}></input>

            </div>
            <div>
                <label>{errorMsg}</label>
            </div>
        </div>
        
        )

}

export default Register;