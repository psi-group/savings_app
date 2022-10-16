import React, { Component } from "react";
import "./App.css";
import Main from "./Main.js";
import Header from "./Header.js";

const App = () => {
  const [searchas, setSearchas] = React.useState("");
  const [selector, setSelector] = React.useState("");
  const [cartItems, setCartItems] = React.useState([]);

  const addCartItem = (name, pickupTime, quantity, image) => {
    const itemIndex = cartItems.findIndex(
      (i) => i.itemName === name && i.pickupTime === pickupTime
    );
    if (itemIndex > -1) {
      const newCart = cartItems.slice();
      newCart[itemIndex].quantity += quantity;
      setCartItems(newCart);
    } else {
      setCartItems((previousItems) => [
        ...previousItems,
        {
          itemName: name,
          pickupTime: pickupTime,
          quantity: quantity,
          image: image
        },
      ]);
    }
    console.log(cartItems);
  };

  const removeCartItem = (index) => {
    
    setCartItems([
      ...cartItems.slice(0, index),
      ...cartItems.slice(index + 1)
    ]);
    
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
        removeCartItem={removeCartItem}
      />
    </div>
  );
};

export default App;
