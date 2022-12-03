import React from 'react';
import { Link, Route, Routes } from 'react-router-dom';
import RestaurantDetails from './RestaurantDetails';

const RestaurantsList = (props) => {

    return (
         
        <ul>
            <div >
                {props.restaurants.map((restaurant) => (
                    <Link to={"/restaurants/" + restaurant.id} className=" border-solid border-2 border-sky-500 inline
                                float-left text-[20px] text-center text-black
                                mb-[30px] p-[5px] ml-[3.5%] w-[240px] h-[225px]
                                hover:shadow-[0_0_2px_1px_rgba(0,151,255,0.7)] font-bold">
                        <li key={restaurant.id}>{restaurant.name}</li>
                        <img className="w-[230px] h-[180px]" src={"https://localhost:7183/userImg/" + restaurant.id + ".jpg"} alt={restaurant.name} />
                    </Link>))}
            </div>
        </ul>
    );
}

export default RestaurantsList