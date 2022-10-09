import React from 'react';
import "./List.css";

const RestaurantsList = (props) => {

    return (
        <ul>
            <div >
            
                {props.restaurants.map((restaurant) => (
                    <div className="container">
                        <a href={"/restaurant/" + restaurant.id}><span></span></a>
                        <li key={restaurant.id}>{restaurant.name}</li>
                        <img className="container" src={restaurant.image} alt={restaurant.name} />
                    </div>))}
             </div>  
        </ul>
    )
}

export default RestaurantsList