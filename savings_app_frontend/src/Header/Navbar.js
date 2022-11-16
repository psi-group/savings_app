import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import ShoppingCart from "../img/shopping-cart.png";
import { Link } from "react-router-dom";
import AuthContext from "../AuthProvider";
import { useContext } from "react";
import useValidateJWT from "../Hooks/useValidateJWT";
import useIsRestaurant from "../Hooks/useIsRestaurant";
import logo from "../img/logo.png"
import { Searchbar } from "./Searchbar";
import MenuItems from "./MenuItems";

const Navbar = (props) => {

    console.log("navbar");

  const isRestaurant = useIsRestaurant();

  const [isProfileDropdownOpen, setIsProfileDropdownOpen] = useState(false);
  const [showCart, setShowCart] = React.useState(false);
  //const [isRestaurant, setIsRestaurant] = React.useState(false);
  const { auth } = useContext(AuthContext);
  const navigate = useNavigate();

  function getName(token) {
    console.log(JSON.parse(window.atob(token.split(".")[1])));
    return JSON.parse(window.atob(token.split(".")[1]))[
      "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
    ];
  }

  function handleLogout() {
    localStorage.removeItem("token");
    navigate("/");
  }

  return (
    <div className="flex py-3 px-3 justify-between items-center w-full">
          <Link to="/" className="logo">
              <img width="55" src={logo} />
      </Link>
      <div className="hidden lg:flex lg:gap-5">
        <Searchbar
          setSearchas={props.setSearchas}
          navigate={navigate}
          setSelector={props.setSelector}
        />
        <MenuItems />
      </div>
      {useValidateJWT() ? (
        <>
          <div className="flex items-center gap-20">
            <div>
              <button
                onClick={() => setIsProfileDropdownOpen(!isProfileDropdownOpen)}
              >
                Hello {getName(localStorage.getItem("token"))}
              </button>
            </div>
            {isProfileDropdownOpen && (
              <div className="w-72 bg-slate-800 border-4 border-black font-bold text-white absolute top-20 right-10 rounded-md flex flex-col flex-grow">
                <button className=" border-b-2 h-14 hover:bg-slate-700">
                  My Orders
                </button>
                <button className=" border-b-2 h-14 hover:bg-slate-700" onClick={(e) => navigate("/profile")}>
                  My Profile
                </button>
                <button className=" h-14 hover:bg-slate-700" onClick={handleLogout}>Log Out</button>
              </div>
            )}
            

            {!isRestaurant ? (
              <div className="w-12 h-12 p-1.5 right-3 top-2 hover:bg-sky-100 hover:rounded-full">
                <Link to="/ShoppingCart">
                  <img
                    src={ShoppingCart}
                    onMouseEnter={() => setShowCart(true)}
                    onMouseLeave={() => setShowCart(false)}
                  />
                </Link>
                {showCart && (
                  <div
                    className="absolute w-96 p-3 h-96 bg-white right-1 z-20 rounded-lg flex flex-col border-sky-500 border-2"
                    onMouseEnter={() => setShowCart(true)}
                    onMouseLeave={() => setShowCart(false)}
                  >
                    <h1 className="self-center text-xl pb-2 pt-2 font-mono font-bold text-sky-500">
                      Shopping Cart
                    </h1>
                    <div className="w-full h-0.5 bg-sky-500 mb-2"></div>
                    <div className="h-60 mb-2 font-mono overflow-y-scroll scrollbar">
                      {props.cartItems.length > 0 ? (
                        props.cartItems.map((cartItem) => {
                          return (
                            <div className="flex flex-col">
                              <h1 className="text-2xl font-bold text-sky-500 tracking-wide">
                                {cartItem.itemName}
                              </h1>
                              <div className="flex gap-2">
                                <img
                                  src={cartItem.image}
                                  className="w-16 h-16 rounded-md border-2 border-black self-center"
                                />
                                <div className="flex flex-col">
                                  <h3 className="font-bold text-xs">
                                    Pickup Time:{" "}
                                  </h3>
                                  <p className="text-xs">
                                    FROM: {cartItem.pickupTime.startTime}
                                    <br></br>
                                    TO: {cartItem.pickupTime.endTime}
                                  </p>
                                  <h3 className="font-bold text-xs">
                                    Quantity:{" "}
                                  </h3>
                                  <p className="text-xs">
                                    {cartItem.quantity} {cartItem.quantityType}
                                  </p>
                                  <h3 className="font-bold text-xs">Price: </h3>
                                  <p className="text-xs">
                                    {cartItem.fullPrice} Eur
                                  </p>
                                </div>
                              </div>
                              <div className="mt-3 mb-3 w-full h-[1px] bg-sky-500">
                                {" "}
                              </div>
                            </div>
                          );
                        })
                      ) : (
                        <p className="text-center font-bold self-center pt-24 text-xl">
                          Shopping Cart Is Empty
                        </p>
                      )}
                    </div>
                    <div className="w-full h-0.5 bg-sky-500"></div>
                    <div className="flex self-center gap-1 mt-2">
                      <p className="text-black">Total: </p>
                      <p className="font-bold text-sky-500">
                        {props.fullSum} Eur
                      </p>
                    </div>
                    <Link to="/ShoppingCart" className="self-center">
                      <button
                        type="button"
                        className="bg-gradient-to-r from-sky-400 to-blue-500 w-30 text-white p-2 font-mono rounded-md hover:font-bold"
                      >
                        Go To Cart
                      </button>
                    </Link>
                  </div>
                )}
              </div>
            ) : (
              <div>
                <button onClick={(e) => navigate("/addProduct")}>
                  Sell Product
                </button>
              </div>
            )}
          </div>
        </>
      ) : (
        <div className="flex items-center">
          <Link className="text-black text-center" to="/register">
            Login/Sign Up
          </Link>
        </div>
      )}
    </div>
  );
};

export default Navbar;
