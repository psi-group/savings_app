import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";
import Main from "./Main.js";
import Header from "./Header.js";

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { products: [], loading: true };
    }

    /*componentDidMount() {
        this.populateProductsData();
    }*/

    

    render() {
      

        return (
            <div className="App">
                <Header/>
                <Main />
            </div>
        );
    }

    async populateProductsData() {
        const response = await fetch('api/products');
        const data = await response.json();
        this.setState({ products: data, loading: false });
    }
}
