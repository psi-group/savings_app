import React from "react";
import  { MENU_ITEMS } from "../Constants";
import { useNavigate } from "react-router-dom";
import ShoppingCart from "../img/shopping-cart.png";
import { Link } from "react-router-dom";

const Navbar = (props) => {
  const [showCart, setShowCart] = React.useState(false);

  const navigate = useNavigate();

  const handleClick = () => {
    navigate("/path/to/push");
  };

  function setSearch() {
    console.log("setinam searcha");
    let value = document.getElementById("search").value;
    props.setSearchas(value);

    var selected = document.getElementById("selector");
    var text = selected.options[selected.selectedIndex].text;

    var currentUrl =
      window.location.href === "https://localhost:3000/"
        ? window.location.href + "products"
        : window.location.href;

    if (currentUrl !== "https://localhost:3000/" + text.toLowerCase()) {
      navigate(text.toLowerCase());

      //window.location.replace("https://localhost:3000/" + text.toLowerCase());
    }
  }

  function setSelect() {
    console.log("setinam");
    var selected = document.getElementById("selector");
    var text = selected.options[selected.selectedIndex].text;
    props.setSelector(text);

    console.log("------------>" + text);
  }

  return (
    <>
      <nav>
        <ul className="menus">
          {MENU_ITEMS.map((menu, index) => {
            return (
              <li className="menu-items" key={index}>
                <Link to={menu.url}>{menu.title}</Link>
              </li>
            );
          })}
        </ul>
      </nav>

      <div>
        <select id="selector" onChange={setSelect}>
          <option>Products</option>
          <option>Restaurants</option>
        </select>
      </div>

      <div>
        <label>Search:</label>
        <input className="border-2 border-sky-100" type="text" id="search" />
        <button onClick={setSearch}>search</button>
      </div>

      <div className="w-12 h-12 p-1.5 absolute right-3 top-2 hover:bg-sky-100 hover:rounded-full">
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
                          <h3 className="font-bold text-xs">Pickup Time: </h3>
                          <p className="text-xs">
                            {cartItem.pickupTime}
                          </p>
                          <h3 className="font-bold text-xs">Quantity: </h3>
                          <p className="text-xs">{cartItem.quantity} {cartItem.quantityType}</p>
                          <h3 className="font-bold text-xs">Price: </h3>
                          <p className="text-xs">{cartItem.fullPrice} Eur</p>
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
              <p className="font-bold text-sky-500">{props.fullSum} Eur</p>
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
    </>
  );
};

export default Navbar;
