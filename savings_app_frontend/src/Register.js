import { useState } from "react";
import { Link } from "react-router-dom";

const Register = () => {
  const defaultImageSrc = "/images/profilePic.jpg";

  const [errorMsg, setErrMsg] = useState("");

  const [isRestaurant, setIsRestaurant] = useState(false);
  const [isAddressInformationVisible, setIsAddressInformationVisible] =
    useState(false);
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

      console.log(isRestaurant);

      const link = "https://localhost:7183/api/" + (isRestaurant ? "restaurants" : "buyers")

    fetch(link, {
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

  return (
    <div className="grid h-screen place-items-center sm:pt-20">
      <div className="text-[18px] w-full sm:w-[600px] flex flex-col sm:border-sky-500 sm:border-2 px-6 py-3 sm:rounded-xl shadow-xl">
        <h1 className="text-center font-bold  mb-3 text-3xl">Register</h1>
        <h3 className="text-center font-bold text-sky-500 mb-3 font-mono">
          {errorMsg}
        </h3>
        <form
          onSubmit={handleRegister}
          className="[&>div]:flex [&>div]:flex-col [&>div>label]:pl-1 [&>div>div]:flex [&>div>div]:gap-2 [&>div>div>input]:flex-1 [&>div>div>input]:p-1 [&>div>input]:border-[1px] [&>div>input]:outline-none [&>div>input]:border-sky-500 [&>div>input]:rounded-xl [&>div>input]:p-1  [&>div>div>input]:border-[1px] [&>div>div>input]:outline-none [&>div>div>input]:border-sky-500 [&>div>div>input]:rounded-xl flex flex-col gap-4"
        >
          <div>
            <label className="pl-1">Email:</label>
            <input
              type="text"
              id="email"
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Email"
              className="focus:border-2"
            ></input>
          </div>

          <div>
            <label>Name:</label>
            <input
              type="text"
              placeholder="Name"
              id="name"
              onChange={(e) => setName(e.target.value)}
              className="focus:border-2"
            ></input>
          </div>

          <div>
            <label>Password:</label>
            <input
              type="password"
              id="password"
              placeholder="Password"
              onChange={(e) => setPassword(e.target.value)}
            ></input>
          </div>
          <button
            type="button"
            onClick={() =>
              setIsAddressInformationVisible(!isAddressInformationVisible)
            }
            className=" bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white h-10 w-full"
          >
            Add primary address information (Optional)
          </button>
          {isAddressInformationVisible && (
            <div className="flex flex-col gap-2 border-2 p-3 rounded-lg border-sky-500">
              <div className="flex">
                <label>Country: </label>
                <input
                  type="text"
                  placeholder="Country"
                  id="country"
                  onChange={(e) =>
                    setAddress({ ...address, country: e.target.value })
                  }
                  className="focus:border-2"
                ></input>
              </div>

              <div>
                <label>City: </label>
                <input
                  type="text"
                  id="city"
                  placeholder="City"
                  onChange={(e) =>
                    setAddress({ ...address, city: e.target.value })
                  }
                  className="focus:border-2"
                ></input>
              </div>

              <div>
                <label>Street Name: </label>
                <input
                  type="text"
                  placeholder="Street Name"
                  id="streetName"
                  onChange={(e) =>
                    setAddress({ ...address, streetName: e.target.value })
                  }
                  className="focus:border-2"
                ></input>
              </div>

              <div>
                <label>House Number: </label>
                <input
                  type="text"
                  id="houseNumber"
                  placeholder="House Number"
                  onChange={(e) =>
                    setAddress({
                      ...address,
                      houseNumber: parseInt(e.target.value),
                    })
                  }
                  className="focus:border-2"
                ></input>
              </div>

              <div>
                <label>Apartment Number: </label>
                <input
                  type="text"
                  id="apartmentNumber"
                  placeholder="Apartment Number"
                  onChange={(e) =>
                    setAddress({
                      ...address,
                      appartmentNumber: parseInt(e.target.value),
                    })
                  }
                  className="focus:border-2"
                ></input>
              </div>

              <div>
                <label>Postal Code: </label>
                <input
                  type="text"
                  placeholder="Postal Code"
                  id="postalCode"
                  onChange={(e) =>
                    setAddress({
                      ...address,
                      postalCode: parseInt(e.target.value),
                    })
                  }
                  className="focus:border-2"
                ></input>
              </div>
            </div>
          )}
          {isRestaurant && (
            <div className="flex flex-col gap-4">
              <div className="flex flex-col h-28">
                <label>Description:</label>
                <input
                  type="text"
                  className="focus:border-2"
                  placeholder="Description"
                  id="description"
                ></input>
              </div>
              <div className="flex flex-col">
                <label>Restaurant Page:</label>
                <input
                  type="text"
                  className="focus:border-2"
                  placeholder="Restaurant Page"
                  id="restaurantPage"
                ></input>
              </div>
            </div>
          )}
          <div>
            <label>Profile Picture: </label>
            <input
              accept="image/*"
              type="file"
              id="picture"
              placeholder="Picture"
              onChange={handleImageChange}
              className="focus:border-2"
            ></input>
            <img
              src={imageSrc}
              className="mt-4 w-36 self-center rounded-full border-3 border-sky-500 "
            ></img>
          </div>

          <div className="flex flex-col gap-2">
            <button
              type="button"
              className=" border-2 border-sky-500 rounded-xl"
              onClick={() => setIsRestaurant(!isRestaurant)}
            >
              <p className="text-[16px]">
                You are registering as a{" "}
                <p className="text-sky-500 inline-block font-bold">
                  {isRestaurant ? "restaurant" : "customer"}
                </p>{" "}
              </p>
              <p className="text-[12px]">Click here to change</p>
            </button>
            <button className=" bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white h-10 w-full">
              Register
            </button>
          </div>
        </form>
        <div className="mt-3 mb-3 bg-sky-500 h-[2px] w-full"></div>
        <h1 className="text-center font-bold">Already have an account?</h1>
        <Link to="/login" className="text-center text-sky-500 hover-sky:700">
          Log In here
        </Link>
      </div>
    </div>
  );
};

export default Register;
