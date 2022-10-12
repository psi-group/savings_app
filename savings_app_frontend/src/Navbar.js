import React from "react";
import { menuItems } from "./menuItems.js";
import { useNavigate } from "react-router-dom";
import ShoppingCart from "./img/shopping-cart.png";
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
          {menuItems.map((menu, index) => {
            return (
              <li className="menu-items" key={index}>
                <a href={menu.url}>{menu.title}</a>
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
        <input type="text" id="search" />
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
            className="absolute w-72 h-96 bg-sky-100 right-1 z-20 rounded-lg flex flex-col border-sky-800 border-2"
            onMouseEnter={() => setShowCart(true)}
            onMouseLeave={() => setShowCart(false)}
          >
            <h1 className="self-center text-lg pb-2 pt-2 font-mono font-bold">
              Shopping Cart
            </h1>
            <div className="w-full h-0.5 bg-sky-800 mb-2"></div>
            <div className="h-64 font-mono"></div>
            <div className="w-full h-0.5 bg-sky-800"></div>
            <Link to="/ShoppingCart" className="self-center">
              <button
                type="button"
                className="mt-3 bg-sky-900 w-30 text-white p-2 font-mono rounded-md hover:font-bold"
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
