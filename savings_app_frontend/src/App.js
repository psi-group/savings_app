import React, { Component } from "react";
import "./App.css";
import Main from "./Main.js";
import Header from "./Header.js";

const App = () => {
  const [searchas, setSearchas] = React.useState("");
  const [selector, setSelector] = React.useState("");
  const [cartItems, setCartItems] = React.useState([]);

  const addCartItem = (item, pickupTime) => {
    cartItems.length == 0
      ? setCartItems([
          {
            itemName: item,
            pickupTime: pickupTime,
          },
        ])
      : setCartItems((previousItems) => [
          ...previousItems,
          {
            itemName: item,
            pickupTime: pickupTime,
          },
        ]);
    console.log(cartItems);
  };

  return (
    <div className="App">
      <Header
        selector={selector}
        setSelector={setSelector}
        searchas={searchas}
        setSearchas={setSearchas}
        cartItems={cartItems}
        addCartItem={addCartItem}
      />
      <Main
        searchas={searchas}
        selector={selector}
        cartItems={cartItems}
        addCartItem={addCartItem}
      />
    </div>
  );
};

export default App;
