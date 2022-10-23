import React, { Component, useEffect } from "react";
import "./App.css";
import Main from "./Routing/Main";
import Header from "./Header/Header";

const App = () => {
  const [searchas, setSearchas] = React.useState("");
  const [selector, setSelector] = React.useState("");
  const [cartItems, setCartItems] = React.useState([]);
  const [fullSum, setFullSum] = React.useState(0);

  useEffect(() => {
    const shoppingCartData = window.sessionStorage.getItem("SHOPPING_CART");
    const fullSumData = window.sessionStorage.getItem("FULL_SUM");
    if (shoppingCartData !== null && fullSumData !== null) {
      setCartItems(JSON.parse(shoppingCartData));
      fullSumData > 0 ? setFullSum(parseFloat(fullSumData)) : setFullSum(0);
    }
  }, []);

  useEffect(
    () => {
      window.sessionStorage.setItem("SHOPPING_CART", JSON.stringify(cartItems));
      window.sessionStorage.setItem("FULL_SUM", JSON.stringify(fullSum));
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
        i.pickupTime.id === pickupTime.id &&
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
      setFullSum(parseFloat(roundNumber(fullSum + unitQuantity * unitPrice, 2)));
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
        setCartItems={setCartItems}
        removeCartItem={removeCartItem}
        roundNumber={roundNumber}
        fullSum={fullSum}
        setFullSum={setFullSum}
      />
    </div>
  );
};

export default App;
