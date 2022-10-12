import React, { Component } from "react";
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";
import Main from "./Main.js";
import Header from "./Header.js";

const App = () => {
  const [searchas, setSearchas] = React.useState("");
  const [selector, setSelector] = React.useState("");
  const [cartItems, setCartItems] = React.useState([]);

  console.log("appas");

  return (
    <div className="App">
      <Header
        selector={selector}
        setSelector={setSelector}
        searchas={searchas}
        setSearchas={setSearchas}
        cartItems={cartItems}
        setCartItems={setCartItems}
      />
      <Main
        searchas={searchas}
        selector={selector}
        cartItems={cartItems}
        setCartItems={setCartItems}
      />
    </div>
  );
};

export default App;
