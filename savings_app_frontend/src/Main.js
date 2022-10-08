import React from 'react';
import { Routes, Route } from 'react-router-dom';

import Home from './Home';
import ProductDetails from './ProductDetails';

const Main = () => {
    return (
        <Routes>
            <Route path='/' element={<Home/>}></Route>
            <Route path='/product/:id' element={<ProductDetails/>}></Route>
        </Routes>
    );
}

export default Main;