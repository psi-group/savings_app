import React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './Home';
import ProductDetails from './ProductDetails';

import Orders from "./Orders";
import Restaurants from './Restaurants';

const Main = (props) => {
    return (
        <Routes>
            <Route path='/' element={<Home searchas={props.searchas }/>}></Route>
            <Route path='/product/:id' element={<ProductDetails />}></Route>
            <Route path='/orders' element={<Orders />}></Route>
            <Route path='/restaurants' element={<Restaurants searchas={props.searchas} />}></Route>
        </Routes>
    );
}

export default Main;