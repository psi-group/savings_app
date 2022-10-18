import React, { Component, useEffect } from "react";
import "./App.css";
import Main from "./Main.js";
import Header from "./Header.js";

const App = () => {
  const [searchas, setSearchas] = React.useState("");
  const [selector, setSelector] = React.useState("");
  const [cartItems, setCartItems] = React.useState([]);
  const [fullSum, setFullSum] = React.useState(0);

  useEffect(() => {
    const shoppingCartData = window.localStorage.getItem("SHOPPING_CART");
    const fullSumData = window.localStorage.getItem("FULL_SUM");
    if (shoppingCartData !== null && fullSumData !== null) {
      setCartItems(JSON.parse(shoppingCartData));
      fullSumData > 0 ? setFullSum(fullSumData) : setFullSum(0);
    }
  }, []);

  useEffect(
    () => {
      window.localStorage.setItem("SHOPPING_CART", JSON.stringify(cartItems));
      window.localStorage.setItem("FULL_SUM", JSON.stringify(fullSum));
    },
    [cartItems],
    [fullSum]
  );



  const roundNumber = (number, decimals) => {
    let newnumber = new Number(number + "").toFixed(parseInt(decimals));
    return parseFloat(newnumber);
  };

  const addCartItem = (
    name,
    pickupTime,
    unitQuantity,
    quantityType,
    quantity,
    image,
    unitPrice
  ) => {
    const itemIndex = cartItems.findIndex(
      (i) =>
        i.itemName === name &&
        i.pickupTime === pickupTime &&
        i.quantityType === quantityType
    );
    if (itemIndex > -1) {
      const newCart = cartItems.slice();
      newCart[itemIndex].quantity = roundNumber(
        newCart[itemIndex].quantity + quantity,
        2
      );
      newCart[itemIndex].unitQuantity += parseInt(unitQuantity);

      newCart[itemIndex].fullPrice = roundNumber(
        newCart[itemIndex].unitQuantity * newCart[itemIndex].unitPrice,
        2
      );
      setCartItems(newCart);
      setFullSum(roundNumber(fullSum + unitQuantity * unitPrice, 2));
    } else {
      setCartItems((previousItems) => [
        ...previousItems,
        {
          itemName: name,
          pickupTime: pickupTime,
          quantity: quantity,
          image: image,
          quantityType: quantityType,
          unitPrice: unitPrice,
          fullPrice: unitPrice * unitQuantity,
          unitQuantity: unitQuantity,
        },
      ]);
      setFullSum(fullSum + unitPrice * unitQuantity);
    }
  };

  const removeCartItem = (index) => {
    setCartItems([...cartItems.slice(0, index), ...cartItems.slice(index + 1)]);
    setFullSum(roundNumber(fullSum - cartItems[index].fullPrice, 2));
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
        fullSum={fullSum}
      />
      <Main
        searchas={searchas}
        selector={selector}
        cartItems={cartItems}
        addCartItem={addCartItem}
        removeCartItem={removeCartItem}
        roundNumber={roundNumber}
        fullSum={fullSum}
      />
    </div>
  );
};

export default App;
