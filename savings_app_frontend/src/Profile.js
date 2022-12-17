import { useEffect, useState } from "react";
import UserLinks from "./Common/UserLinks";
import { CircleSpinnerOverlay } from "react-spinner-overlay";

function getId(token) {
  //console.log(JSON.parse(window.atob(token.split(".")[1])));
  return JSON.parse(window.atob(token.split(".")[1]))["Id"];
}

function getRole(token) {
  if (token != null)
    return JSON.parse(window.atob(token.split(".")[1]))[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ];
  else return null;
}

function getName(token) {
  //console.log(JSON.parse(window.atob(token.split(".")[1])));
  return JSON.parse(window.atob(token.split(".")[1]))[
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
  ];
}

const Profile = () => {
  const [profile, setProfile] = useState(null);
  const [loading, setLoading] = useState(true);
  console.log(profile);
  useEffect(() => {
    const profileRole =
      getRole(localStorage.getItem("token")) == "seller"
        ? "restaurants"
        : "buyers";
    fetch(
      "https://localhost:7183/api/" +
        profileRole +
        "/private/" +
        getId(localStorage.getItem("token")),
      {
        headers: {
          "Access-Control-Allow-Origin": "*",
          Authorization: "Bearer " + localStorage.getItem("token"),
        },
      }
    )
      .then(async (response) => {
        if (!response.ok) {
          throw new Error(
            `This is an HTTP error: The status is ${response.status}`
          );
        }
        const res = await response.json();
        setProfile(res);
        setLoading(false);
      })
      .catch((err) => {});
  }, []);

  return !loading ? (
    <div className="flex justify-center  mt-40 gap-3 ">
      <div className="flex flex-col items-start justify-start ">
    
        <UserLinks />
      </div>
      <div>
        <div className=" text-left   text-3xl font-bold "> Your Profile</div>
        <div className="flex flex-col gap-3">
        <div className="float-left w-[600px] h-[290px] rounded-xl  bg-slate-100 border-solid border-2 border-sky-500 flex items-center justify-center px-5">
          <div className="card-body  w-[500px] h-[300px] text-xm ">
            <div className="row  mt-3 mb-3 border-solid  h-[30px]  border-2 rounded-xl flex items-center border-black">
              <div className="col-sm-5">
                <h6 className="mb-0">Name:</h6>
              </div>
              <div className="col-sm-5 text-secondary"> {profile.name}</div>
            </div>
            <div className="row  mt-3 mb-3 border-solid  h-[30px]  border-2 rounded-xl flex items-center border-black">
              <div className="col-sm-5">
                <h6 className="mb-0">Email:</h6>
              </div>
              <div className="col-sm-5 text-secondary ">
                {" "}
                {profile.userAuth.email}
              </div>
            </div>
            <div>
              <div>
                <div>
                  <div className="row  mt-3 mb-3 border-solid h-[30px]  border-2 rounded-xl flex items-center border-black">
                    <div className="col-sm-3 mt-[3.5px]">
                      <h6 className="mb-0">Address:</h6>
                    </div>
                    <div className="col-sm-9 text-secondary"> {} </div>
                  </div>
                  <div>
                    <div className="flex items-center justify-center">
                      <div className="col-sm-3">
                        <button className="bg-sky-500 p-2 self-center px-5 mt-5 rounded-xl text-white text-xl">Edit</button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          
        </div>
        <div className="self-center w-[275px] h-[325px]   border-solid border-2 border-sky-500 rounded-xl bg-slate-100">
          <div className="d-flex flex-column align-items-center text-center mt-4 ">
            <img
              src={
                profile.imageUrl == null
                  ? "https://savingsapp.blob.core.windows.net/userimages/profilePic.jpg"
                  : profile.imageUrl
              }
              alt="Admin"
              className="rounded-circle"
              width="150"
            />
            <div className="mt-3">
              <h4>{getName(localStorage.getItem("token"))}</h4>
              <p className="text-secondary mb-1">Country: Lithuania</p>
              <p className="text-muted font-size-sm">City: Vilnius</p>
            </div>
          </div>
        </div>
        </div>
        
      </div>
    </div>
  ) : (
    <CircleSpinnerOverlay loading={loading} color="#0ea5e9" />
  );
};

export default Profile;
