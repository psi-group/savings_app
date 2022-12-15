import React, { Component, useEffect } from "react";
import "./App.css";
import Main from "./Routing/Main";
import Header from "./Header/Header";
import { Routes, Route } from 'react-router-dom';
import Home from './Home';
import ProductDetails from './Product/ProductDetails';
import Orders from "./Orders/Orders";
import Restaurants from './Restaurant/Restaurants';
import RestaurantDetails from './Restaurant/RestaurantDetails';
import ShoppingCart from './ShoppingCart';
import Register from "./Register";
import Login from "./Login";
import ProtectedRoute from "./ProtectedRoute";
import Profile from "./Profile";
import AddProduct from "./AddProduct";
import UnauthorizedSeller from "./UnauthorizedSeller";
import UnauthorizedBuyer from "./UnauthorizedBuyer";
import Unauthorized from "./Unauthorized";
import Snackbar from '@mui/material/Snackbar';

const App = () => {
  const [searchas, setSearchas] = React.useState("");
  const [selector, setSelector] = React.useState("");
  const [cartItems, setCartItems] = React.useState([]);
    const [fullSum, setFullSum] = React.useState(0);
    const [snackOn, setSnackOn] = React.useState(false);
    const [snackMessage, setSnackMessage] = React.useState("");


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

  

    console.log("pradzia");
    console.log(isValidUrl("HELLO"));


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
                setSnackOn={setSnackOn}
                setSnackMessage={setSnackMessage}
            />
            <Routes>

                    <Route path='/unauthorized' element={<Unauthorized></Unauthorized>} ></Route>
                    <Route path='/unauthorizedSeller' element={<UnauthorizedSeller></UnauthorizedSeller>} ></Route>
                    <Route path='/unauthorizedBuyer' element={<UnauthorizedBuyer></UnauthorizedBuyer>} ></Route>

                    
                    <Route path='/' element={<Home searchas={searchas} isValidUrl={isValidUrl} />}></Route>
                    
                    

                    <Route path='/register' element={<Register setSnackOn={setSnackOn} setSnackMessage={setSnackMessage}></Register>} ></Route>
                    <Route path='/login' element={<Login setSnackOn={setSnackOn} setSnackMessage={setSnackMessage}></Login>} ></Route>
                    
                    <Route path='/product/:id' element={<ProductDetails cartItems={cartItems} addCartItem={addCartItem} roundNumber={roundNumber} />}></Route>
                    <Route path='/orders' element={<Orders />}></Route>
                    <Route path='/restaurants' element={<Restaurants searchas={searchas} />}></Route>
                    <Route path='/restaurants/:id' element={<RestaurantDetails />}></Route>

                    <Route element={<ProtectedRoute roles={["seller"]} />}>
                        <Route path='/addProduct' element={<AddProduct setSnackOn={setSnackOn} setSnackMessage={setSnackMessage} />}></Route>
                    </Route>
                    
                    <Route element={<ProtectedRoute roles={["seller", "buyer"]} />}>
                        <Route path='/profile' element={<Profile />}></Route>
                    </Route>

                    <Route element={<ProtectedRoute roles={["buyer"]} />}>
                        <Route path='/shoppingCart' element={<ShoppingCart setSnackOn={setSnackOn} setSnackMessage={setSnackMessage} cartItems={cartItems} setCartItems={setCartItems} addCartItem={addCartItem} removeCartItem={removeCartItem} fullSum={fullSum} setFullSum={setFullSum} />}></Route>
                    </Route>
                    
            </Routes>
            <Snackbar
                open={snackOn}
                autoHideDuration={4000}
                message={snackMessage}
                onClose={(e) => {
                    setSnackOn(false);
                }}
            />
        </>
  );
};

export default App;
