import React, { Component } from 'react';
import "./Restaurants.css";
import RestaurantsList from "./RestaurantsList";


export default class Restaurants extends Component {

    constructor(props) {
        super(props);
        this.state = { restaurants: [], loading: true };
        
    }
    componentDidMount() {
        console.log("Res mounted");
        this.populateRestaurantsData();
    }

   
    renderRestaurantsList(restaurants) {
        return (
            <div>
                <RestaurantsList restaurants={restaurants} />
            </div>
        )
    }

    render(restaurants) {
        let content = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : this.renderRestaurantsList(this.state.restaurants);

        return (
            <div>
                <h1 className="all">All Restaurants</h1>
                {content}
            </div>

        )

    }
    async populateRestaurantsData() {
        const response = await fetch('https://localhost:7183/api/restaurants');
        const data = await response.json();
        this.setState({ restaurants: data, loading: false })
    }
}