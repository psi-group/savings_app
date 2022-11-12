import React, { Component, useEffect } from "react";
import "./App.css";
import Main from "./Routing/Main";
import Header from "./Header/Header";
import { Routes, Route } from 'react-router-dom';
import Home from './Home';
import ProductDetails from './Product/ProductDetails';
import Orders from "./Orders";
import Restaurants from './Restaurant/Restaurants';
import RestaurantDetails from './Restaurant/RestaurantDetails';
import ShoppingCart from './ShoppingCart';
import Register from "./Register";
import Login from "./Login";
import ProtectedRoute from "./ProtectedRoute";
import Profile from "./Profile";
import AddProduct from "./AddProduct";

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
      unitPrice,
      restaurant,
    product
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
            restaurant: restaurant,
          product: product
        },
      ]);
      setFullSum(fullSum + unitPrice * unitQuantity);
    }
  };

  const removeCartItem = (index) => {
    setCartItems([...cartItems.slice(0, index), ...cartItems.slice(index + 1)]);
    setFullSum(roundNumber(fullSum - cartItems[index].fullPrice, 2));
  };

  const isValidUrl = (string) => {
    try {
      new URL(string);
      return true;
    } catch (err) {
      return false;
    }
  }

    return (

        <>
            <Header
                selector={selector}
                setSelector={setSelector}
                searchas={searchas}
                setSearchas={setSearchas}
                cartItems={cartItems}
                addCartItem={addCartItem}
                fullSum={fullSum}
            />
            <Routes>
                { /* public paths */}


                    <Route element={<ProtectedRoute />}>
                        <Route path='/' element={<Home searchas={searchas} isValidUrl={isValidUrl} />}></Route>
                    </Route>
                    
                
                    
                    <Route path='/register' element={<Register></Register>} ></Route>
                    <Route path='/login' element={<Login></Login>} ></Route>

                    <Route path='/products' element={<Home searchas={searchas} />}></Route>
                    <Route path='/product/:id' element={<ProductDetails cartItems={cartItems} addCartItem={addCartItem} roundNumber={roundNumber} />}></Route>
                    <Route path='/orders' element={<Orders />}></Route>
                    <Route path='/restaurants' element={<Restaurants searchas={searchas} />}></Route>
                    <Route path='/restaurants/:id' element={<RestaurantDetails />}></Route>

                    <Route path='/addProduct' element={<AddProduct />}></Route>


                    <Route path='/profile' element={<Profile />}></Route>

                    <Route element={<ProtectedRoute />}>
                        <Route path='/shoppingCart' element={<ShoppingCart cartItems={cartItems} setCartItems={setCartItems} addCartItem={addCartItem} removeCartItem={removeCartItem} fullSum={fullSum} setFullSum={setFullSum} />}></Route>
                    </Route>
                    
            </Routes>
        </>
  );
};

export default App;
