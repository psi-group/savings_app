import React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './Home';
import ProductDetails from './ProductDetails';

import Orders from "./Orders";

const Main = () => {
    return (
        <Routes>
            <Route path='/' element={<Home/>}></Route>
            <Route path='/product/:id' element={<ProductDetails />}></Route>
            <Route path='/orders' element={<Orders />}></Route>
        </Routes>
    );
}

export default Main;