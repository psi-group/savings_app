import React, { Component } from 'react';


export default class Restaurants extends Component {

    constructor(props) {
        super(props);
        this.state = { restaurants: [], loading: true };
        
    }
    componentDidMount() {
        console.log("Res mounted");
        this.populateRestaurantsData();
    }

    render(restaurants) {
        return (
            <h1>All Restaurants</h1>
        )
            
    }

    async populateRestaurantsData() {
        const response = await fetch('https://localhost:7183/api/restaurants');
        const data = await response.json();
        this.setState({ restaurants: data, loading: false })
    }
}
