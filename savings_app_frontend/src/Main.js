import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Home from './Home';
import ProductDetails from './ProductDetails';
import Orders from "./Orders";
import RestaurantsDetails from './RestaurantDetails';
import Restaurants from './Restaurants';
import RestaurantDetails from './RestaurantDetails';
import ShoppingCart from './ShoppingCart';

const Main = (props) => {
    return (
        <Routes>
            <Route path='/' element={<Home searchas={props.searchas} />}></Route>
            <Route path='/products' element={<Home searchas={props.searchas} />}></Route>
            <Route path='/product/:id' element={<ProductDetails cartItems={props.cartItems} addCartItem={props.addCartItem}/>}></Route>
            <Route path='/orders' element={<Orders />}></Route>
            <Route path='/restaurants' element={<Restaurants searchas={props.searchas} />}></Route>
            <Route path='/restaurants/:id' element={<RestaurantDetails />}></Route>
            <Route path='/shoppingCart' element={<ShoppingCart />}></Route>
        </Routes>
    );
}

export default Main;