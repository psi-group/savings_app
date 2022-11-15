import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Home from '../Home';
import ProductDetails from '../Product/ProductDetails';
import Orders from "../Orders";
import Restaurants from '../Restaurant/Restaurants';
import RestaurantDetails from '../Restaurant/RestaurantDetails';
import ShoppingCart from '../ShoppingCart';
import Register from "../Register";
import Login from "../Login";


const Main = (props) => {
    return (
        <Routes>
            <Route path='/' element={<Home searchas={props.searchas} isValidUrl={props.isValidUrl}/>}></Route>
            <Route path='/register' element={<Register></Register>} ></Route>
            <Route path='/login' element={<Login></Login>} ></Route>

            <Route path='/products' element={<Home searchas={props.searchas}/>}></Route>
            <Route path='/product/:id' element={<ProductDetails cartItems={props.cartItems} addCartItem={props.addCartItem} roundNumber={props.roundNumber} />}></Route>
            <Route path='/orders' element={<Orders />}></Route>
            <Route path='/restaurants' element={<Restaurants searchas={props.searchas} />}></Route>
            <Route path='/restaurants/:id' element={<RestaurantDetails />}></Route>
            <Route path='/shoppingCart' element={<ShoppingCart cartItems={props.cartItems} setCartItems={props.setCartItems} addCartItem={props.addCartItem} removeCartItem={props.removeCartItem} fullSum={props.fullSum} setFullSum={props.setFullSum}/>}></Route>
        </Routes>
    );
}

export default Main;