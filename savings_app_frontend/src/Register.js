
import { useState } from 'react';
import Switch from "react-switch";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";


const Register = () => {

    const navigate = useNavigate();

    const defaultImageSrc = '/images/profilePic.jpg';

    const [errorMsg, setErrMsg] = useState("");

    const [isRestaurant, setIsRestaurant] = useState(false);
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [name, setName] = useState("");
    const [imageSrc, setImageSrc] = useState(defaultImageSrc);
    const [imageFile, setImageFile] = useState();

    const [address, setAddress] = useState({
        country: "",
        city: "",
        streetName: "",
        houseNumber: 0,
        appartmentNumber: 0,
        postalCode: 0

    });


    const handleRegister = (e) => {
        e.preventDefault();
        console.log("regiter");
        const formData = new FormData();
        
        //let userAuth = { 'email': email, "password": password };
        formData.append('userAuth[email]', email);
        formData.append('userAuth[password]', password);
        formData.append('name', name);
        formData.append('imageFile', imageFile);
        console.log(address);
        formData.append('address[country]', address.country);
        formData.append('address[city]', address.city);
        formData.append('address[streetName]', address.streetName);
        formData.append('address[houseNumber]', address.houseNumber);
        formData.append('address[appartmentNumber]', address.appartmentNumber);
        formData.append('address[postalCode]', address.postalCode);






        fetch("https://localhost:7183/api/auth/register/buyer",
            {
                method: "POST",
                headers: { "Access-Control-Allow-Origin": "*" },
                body: formData
            }).
            then(async response => {

                const isJson = response.headers.get('content-type')?.includes('application/json');

                console.log(isJson);

                console.log(response);
                if (!response.ok) {

                    console.log(response.statusText);
                    setErrMsg("Incorrect register inputs");

                }
                else {
                    setErrMsg("");
                }
            }).catch(error => {

                setErrMsg(error);
            });
    };


    const handleImageChange = (e) => {
        console.log("change");
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setImageFile(imageFile);
                setImageSrc(x.target.result);
                console.log(x.target.result);
            }
            reader.readAsDataURL(imageFile);
        }
        else {
            setImageFile(null);
            setImageSrc(defaultImageSrc);
        }
    }
    

    const redirectToLogin = () => {
        navigate("/login");
    }

    return (

        <div>

            <div>
                <button onClick={redirectToLogin }>Already registed?</button>
            </div>


            <div>
                <label>{errorMsg}</label>
            </div>
            <p>Are You registering a restaurant?</p>
            <Switch onChange={(e) => setIsRestaurant(e)} checked={isRestaurant}/>
            <div className="text-[20px] ml-10 mt-10">
                <form onSubmit={handleRegister}>
                    <label>Email:</label>
                    <input className=" mb-2 ml-[50px] border-2" type="text" id="email" onChange={(e) => setEmail(e.target.value)}></input><br></br>
                    <label>Password:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="password" id="password" onChange={(e) => setPassword(e.target.value)} ></input><br></br>
                    <label>Name:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="name" onChange={(e) => setName(e.target.value)} ></input><br></br>

                    <label>Country:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="country" onChange={(e) => setAddress({ ...address, country : e.target.value })} ></input><br></br>

                    <label>City:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="city" onChange={(e) => setAddress({ ...address, city : e.target.value })} ></input><br></br>

                    <label>Street Name:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="streetName" onChange={(e) => setAddress({ ...address, streetName : e.target.value })} ></input><br></br>

                    <label>House Number:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="houseNumber" onChange={(e) => setAddress({ ...address, houseNumber : parseInt(e.target.value) })} ></input><br></br>

                    <label>Appartment Number:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="country" onChange={(e) => setAddress({ ...address, appartmentNumber: parseInt(e.target.value) })} ></input><br></br>

                    <label>Postal Code:</label>
                    <input className=" mb-2 ml-[10px] border-2" type="text" id="country" onChange={(e) => setAddress({ ...address, postalCode: parseInt(e.target.value) })} ></input><br></br>



                    <label>Profile Picture:</label>
                    <input className=" mb-2 ml-[10px] border-2" accept="image/*" type="file" id="picture" onChange={handleImageChange} ></input><br></br>
                    <img src={imageSrc} ></img>

                    <button className=" mb-2   border-2">Sign Up</button>
                </form>
            </div>
        </div>
        
        )

}

export default Register;