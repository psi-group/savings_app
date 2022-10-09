import { Link, useNavigate } from 'react-router-dom';
import "./Filter.css";
import "./useFetchData.js";
import React, { useEffect } from 'react';
import useFetchData from './useFetchData.js';



const Filter = (props) => {

    const [categories] = useFetchData("https://localhost:7183/api/categories");

    return (

        <>
            <div class="filterContainer">
                {categories &&
                    categories.map((category) => {
                        return <p key={category.id} class="categories">{category.categoryName}</p>
                    })}
            </div>
        </>

    );





};

export default Filter;
