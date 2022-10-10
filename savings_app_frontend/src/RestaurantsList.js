import React from 'react';

const RestaurantsList = (props) => {

    return (
        <ul>
            <div >
            
                {props.restaurants.map((restaurant) => (
                    <a className="mb-[30px] border-solid border-2 border-green-500 inline block float-left text-[20px] text-center text-black mb-[30px] p-[5px] ml-[3.5%] w-[240px] h-[225px] hover:shadow-[0_0_2px_1px_rgba(0,116,0,0.7)]"  href={"/restaurant/" + restaurant.id}>
                        <li key={restaurant.id}>{restaurant.name}</li>
                        <img className="w-[230px] h-[180px]" src={restaurant.image} alt={restaurant.name} />
                    </a>))}
             </div>  
        </ul>
    )
}

export default RestaurantsList