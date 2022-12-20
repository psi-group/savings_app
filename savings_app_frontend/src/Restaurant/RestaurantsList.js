import React from "react";
import { Link, Route, Routes } from "react-router-dom";


const RestaurantsList = (props) => {
    console.log(props.restaurants);
  return (
    <ul>
      <div>
        {props.restaurants.map((restaurant) => (
          <Link
            to={"/restaurants/" + restaurant.id}
            className=" border-solid border-2 border-sky-500 
                                float-left text-[20px] text-center text-black
                                mb-[30px] p-[5px] ml-[3.5%] w-[260px] flex items-center justify-center
                                hover:shadow-[0_0_2px_1px_rgba(0,151,255,0.7)] font-bold rounded-lg h-[300px] "
          >
            <div className="flex flex-col gap-3 items-center justify-center">
            <li key={restaurant.id}>{restaurant.name}</li>
            <img
              className="w-48 h-48"
              src={
                  restaurant.imageUrl == null ?
                      "https://savingsapp.blob.core.windows.net/userimages/profilePic.jpg" :
                      restaurant.imageUrl
              }
              alt={restaurant.name}
            />
            <p className="text-sky-500 text-base ">{restaurant.open ? "Restaurant is currently open": "Restaurant is currently closed"}</p>
            </div>
          </Link>
        ))}
      </div>
    </ul>
  );
};

export default RestaurantsList;
