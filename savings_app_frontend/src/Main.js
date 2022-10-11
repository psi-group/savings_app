import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Home from './Home';
import ProductDetails from './ProductDetails';
import Orders from "./Orders";
import RestaurantsDetails from './RestaurantDetails';
import Restaurants from './Restaurants';
import RestaurantDetails from './RestaurantDetails';

const Main = (props) => {
    return (
        <Routes>
            <Route path='/' element={<Home searchas={props.searchas }/>}></Route>
            <Route path='/product/:id' element={<ProductDetails />}></Route>
            <Route path='/orders' element={<Orders />}></Route>
            <Route path='/restaurants' element={<Restaurants searchas={props.searchas} />}></Route>
            <Route path='/restaurants/:id' element={<RestaurantDetails />}></Route>
        </Routes>
    );
}

export default Main;