import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";

export default class Orders extends Component {

    constructor(props) {
        super(props);
        this.state = { orders: [], loading: true };
    }

    componentDidMount() {
        console.log("mounted");
        this.populateProductsData();
    }

    async getOrders() {

        console.log("pressed");

        let value = document.getElementById('fname').value;

        const response = await fetch('https://localhost:7183/api/orders/byClientId/' + value);
        const data = await response.json();
        console.log(data);

    }

    static renderForecastsTable(products) {
        return (


            <div className="main">
                <h1>React Search</h1>

                <div>


                    <label >Enter your user ID:</label>
                    <input type="text" id="fname"/>
                    <button onClick={this.getOrders }>submit</button> 


                    

                </div>


            </div>


        );
    }

    render() {




        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : Orders.renderForecastsTable(this.state.products);

        return (
            <div className="main">
                <h1>React Search</h1>

                <div>


                    <label >Enter your user ID:</label>
                    <input type="text" id="fname" />
                    <button onClick={this.getOrders}>submit</button>




                </div>


            </div>
        );
    }

    async populateProductsData() {
        console.log("this is called");
        const response = await fetch("https://localhost:7183/api/products");
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }
}
