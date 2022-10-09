import React from 'react';
import "./Restaurants.css";

const RestaurantsList = (props) => {

    return (
        <ul>
            <div >
            
                {props.restaurants.map((restaurant) => (
                    <a className="restaurants"  href={"/restaurant/" + restaurant.id}>
                        <li key={restaurant.id}>{restaurant.name}</li>
                        <img className="img" src={restaurant.image} alt={restaurant.name} />
                    </a>))}
             </div>  
        </ul>
    )
}

export default RestaurantsList