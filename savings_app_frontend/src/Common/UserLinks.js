import React from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";

const UserLinks = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const pathname = location.pathname;
  const isOrdersPage = pathname === "/orders" ? true : false;
  const isProfilePage = pathname ==="/profile" ? true : false;

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <div className="w-[250px] h-[250px] bg-slate-100 border-sky-500 border-2 rounded-xl items-center justify-evenly text-black text-3xl  flex flex-col ">
      <Link to="/profile" className={`hover:text-sky-500 ${isProfilePage && "text-sky-500"}`} >
        My Profile
      </Link>
      <Link to="/orders" className={`hover:text-sky-500 ${isOrdersPage && "text-sky-500"}`}>
        My Orders
      </Link>
      <button onClick={handleLogout} className="hover:text-sky-500">Log Out</button>
    </div>
  );
};

export default UserLinks;
