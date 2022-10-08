import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";

export default class Home extends Component {

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    componentDidMount() {
        console.log("mounted");
        this.populateProductsData();
    }

    static renderForecastsTable(products) {
        return (


                <div className="main">
                    <h1>React Search</h1>

                    <div>

                        
                        <SearchAndDisplay products={products} />

                    </div>


                </div>


        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : Home.renderForecastsTable(this.state.products);

        return (
            <div>
                <h1 id="tabelLabel" >Savings app</h1>
                {contents}
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
