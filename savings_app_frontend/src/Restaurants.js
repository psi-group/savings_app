import React, { Component } from 'react';
import RestaurantsList from "./RestaurantsList";


function Restaurants(props) {

    const [content, setContent] = React.useState([]);
    const [loading, setLoading] = React.useState([]);

    const renderRestaurantsList = (restaurants) => {
        console.log(restaurants);
        return (
            <div>
                <RestaurantsList restaurants={restaurants} />
            </div>
        )
    }

    React.useEffect(() => {
        
        console.log("useEffect");
        let url = 'https://localhost:7183/api/restaurants/filter?search=' + props.searchas;
        fetch(url)
            .then(res => res.json())
            .then(res => { setContent(res); setLoading(false) })
            .catch(err => console.log(err));
    }, [props.searchas]);

       

    let contents = loading
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : renderRestaurantsList(content);

    
    return <div>
        <h1 className="mt-4 text-center mb-[30px] text-[30px]">Restaurants</h1>
        {contents}
    </div>

    

    
}

export default Restaurants;