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
import userIcon from "../img/userIcon.png";
import restaurantIcon from "../img/restaurantIcon.png";

const Navbar = (props) => {

  const isRestaurant = useIsRestaurant();

  const [isProfileDropdownOpen, setIsProfileDropdownOpen] = useState(false);
  const [showCart, setShowCart] = React.useState(false);
  //const [isRestaurant, setIsRestaurant] = React.useState(false);
  const navigate = useNavigate();


  function handleLogout() {
    localStorage.removeItem("token");
    navigate("/");
  }

  return (
    <div className="flex py-3 px-3 justify-between items-center w-full">
          <Link to="/" className="logo">
              <img width="55" src={logo} />
      </Link>
      <div className="hidden lg:flex lg:gap-1 lg:flex-col lg:items-center w-full ">
        <Searchbar
          setSearchas={props.setSearchas}
          navigate={navigate}
          setSelector={props.setSelector}
        />
        {/* <MenuItems /> */}
      </div>
      {useValidateJWT() ? (
        <>
          <div className="flex items-center gap-1" >
            <div>
              <Link to="/restaurants">
              <img src={restaurantIcon} className="w-[48px] pb-2 p-1 hover:bg-sky-100 hover:rounded-full"/> 
              </Link>
            </div>
            <div>
                <img src={userIcon} className="w-[48px] pb-2 p-1 hover:bg-sky-100 hover:rounded-full" onMouseEnter={() => setIsProfileDropdownOpen(true)}
                    onMouseLeave={() => setIsProfileDropdownOpen(false)}/>
            </div>
            {isProfileDropdownOpen && (
              <div className="w-64 bg-white border-2 border-sky-500 font-bold text-sky-500 absolute top-14 right-1 rounded-md flex flex-col flex-grow" onMouseEnter={() => setIsProfileDropdownOpen(true)}
              onMouseLeave={() => setIsProfileDropdownOpen(false)}>
                              <button className=" border-b-2 h-14 hover:bg-sky-500 hover:text-white" onClick={(e) => navigate("/orders")} >
                  My Orders
                </button>
                <button className=" border-b-2 h-14 hover:bg-sky-500 hover:text-white" onClick={(e) => navigate("/profile")}>
                  My Profile
                </button>
                <button className=" h-14 hover:bg-sky-500 hover:text-white" onClick={handleLogout}>Log Out</button>
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
                    className="absolute top-14 w-96 p-3 h-96 bg-white right-1 z-20 rounded-lg flex flex-col border-sky-500 border-2"
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
                                          src={cartItem.product.imageUrl == null ?
                                              "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg" :
                                              cartItem.product.imageUrl}
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
        <div className="flex items-center gap-3">
          <Link className="text-black flex w-12 hover:text-sky-500" to="/login">
            Log In
          </Link>
          <Link className="text-white text-center bg-sky-500 p-2 rounded-xl hover:bg-sky-700" to="/register">
            Register
          </Link>
        </div>
      )}
    </div>
  );
};

export default Navbar;
