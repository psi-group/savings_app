import { useState } from "react";
import Switch from "react-switch";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const navigate = useNavigate();

  const defaultImageSrc = "/images/profilePic.jpg";

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
    postalCode: 0,
  });

  const handleRegister = (e) => {
    e.preventDefault();
    console.log("regiter");
    const formData = new FormData();

    //let userAuth = { 'email': email, "password": password };
    formData.append("userAuth[email]", email);
    formData.append("userAuth[password]", password);
    formData.append("name", name);
    formData.append("imageFile", imageFile);
    console.log(address);
    formData.append("address[country]", address.country);
    formData.append("address[city]", address.city);
    formData.append("address[streetName]", address.streetName);
    formData.append("address[houseNumber]", address.houseNumber);
    formData.append("address[appartmentNumber]", address.appartmentNumber);
    formData.append("address[postalCode]", address.postalCode);

    fetch("https://localhost:7183/api/auth/register/buyer", {
      method: "POST",
      headers: { "Access-Control-Allow-Origin": "*" },
      body: formData,
    })
      .then(async (response) => {
        const isJson = response.headers
          .get("content-type")
          ?.includes("application/json");

        console.log(isJson);

        console.log(response);
        if (!response.ok) {
          console.log(response.statusText);
          setErrMsg("Incorrect register inputs");
        } else {
          setErrMsg("");
        }
      })
      .catch((error) => {
        setErrMsg(error);
      });
  };

  const handleImageChange = (e) => {
    console.log("change");
    if (e.target.files && e.target.files[0]) {
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        setImageFile(imageFile);
        setImageSrc(x.target.result);
        console.log(x.target.result);
      };
      reader.readAsDataURL(imageFile);
    } else {
      setImageFile(null);
      setImageSrc(defaultImageSrc);
    }
  };

  const redirectToLogin = () => {
    navigate("/login");
  };

  return (
    <div className="flex  flex-col justify-center mx-96">
      <div className="mt-3 flex flex-col align-middle gap-3">
        <h1 className="text-center font-bold">Already registered?</h1>
        <button
          onClick={redirectToLogin}
          className="border-3 border-sky-800 font-bold hover:bg-sky-800 hover:text-white h-10"
        >
          Login
        </button>
      </div>

      <div className="bg-sky-800 h-[2px] w-full mt-3 mb-3"></div>

      <label className="text-center font-bold text-red-800 mb-3 font-mono">
        {errorMsg}
      </label>

      <div className="flex flex-col gap-2">
        <h1 className="font-bold">Are You registering a restaurant?</h1>
        <Switch
          className="pb-3"
          onChange={(e) => setIsRestaurant(e)}
          checked={isRestaurant}
        />
      </div>

      <div className="text-[20px]">
        <form
          onSubmit={handleRegister}
          className="flex flex-col justify-center [&>div>input]:border-2 [&>div>input]:border-sky-800 
          [&>div>label]:border-2 [&>div>label]:border-black [&>div>label]:border-b-0 [&>div>label]:whitespace-pre [&>div>label]:w-[170px] [&>div>label]:text-[16px] 
          [&>div>label]:font-serif"
        >
          <div className="grid">
            <input
              className="peer"
              type="text"
              id="email"
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Email"
            ></input>
            <br></br>
            <label
              className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white "
              htmlFor="email"
            >
              Email:
            </label>
          </div>

          <div className="grid">
            <input
              className="peer"
              type="password"
              id="password"
              placeholder="Password"
              onChange={(e) => setPassword(e.target.value)}
            ></input>
            <br></br>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              Password:
            </label>
          </div>

          <div className="grid">
            <input
              className="peer"
              type="text"
              placeholder="Name"
              id="name"
              onChange={(e) => setName(e.target.value)}
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              Name:
            </label>
          </div>
          <br></br>

          <div className="grid">
            <input
              className="peer"
              type="text"
              placeholder="Country"
              id="country"
              onChange={(e) =>
                setAddress({ ...address, country: e.target.value })
              }
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              Country:
            </label>
          </div>
          <br></br>

          <div className="grid">
            <input
              type="text"
              id="city"
              className="peer"
              placeholder="City"
              onChange={(e) => setAddress({ ...address, city: e.target.value })}
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              City:
            </label>
          </div>
          <br></br>

          <div className="grid">
            <input
              className="peer"
              type="text"
              placeholder="Street Name"
              id="streetName"
              onChange={(e) =>
                setAddress({ ...address, streetName: e.target.value })
              }
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              Street Name:
            </label>
          </div>
          <br></br>

          <div className="grid">
            <input
              type="text"
              className="peer"
              id="houseNumber"
              placeholder="House Number"
              onChange={(e) =>
                setAddress({
                  ...address,
                  houseNumber: parseInt(e.target.value),
                })
              }
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              House Number:
            </label>
          </div>
          <br></br>

          <div className="grid">
            <input
              type="text"
              className="peer"
              id="country"
              placeholder="Country"
              onChange={(e) =>
                setAddress({
                  ...address,
                  appartmentNumber: parseInt(e.target.value),
                })
              }
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              Appartment Number:
            </label>
          </div>
          <br></br>

          <div className="grid">
            <input
              type="text"
              className="peer"
              placeholder="City"
              id="city"
              onChange={(e) =>
                setAddress({ ...address, postalCode: parseInt(e.target.value) })
              }
            ></input>
            <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">
              Postal Code:
            </label>
          </div>
          <br></br>

            <div className="grid">
                 
          
          <input
            accept="image/*"
            type="file"
            id="picture"
            className="peer"
            placeholder="Picture"
            onChange={handleImageChange}
          ></input>
          <label className="peer-focus:bg-sky-800 row-start-1 peer-focus:text-white ">Profile Picture:</label>
          </div> 
          <br></br>
          <img src={imageSrc}></img>

          <button className="border-3 border-sky-800 font-bold hover:bg-sky-800 hover:text-white h-10 my-3">Sign Up</button>
        </form>
      </div>
    </div>
  );
};

export default Register;
