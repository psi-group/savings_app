import React, { Component } from 'react';
import "./Orders.css";
import SearchAndDisplay from "./SearchAndDisplay";

export default class Orders extends Component {

    constructor(props) {
        super(props);
        this.state = { orders: [], loading: true };
        this.getOrders = this.getOrders.bind(this);
    }


    async getOrders() {

        console.log("pressed");

        let value = document.getElementById('fname').value;

        const response = await fetch('https://localhost:7183/api/orders/byBuyerId/' + value);
        const data = await response.json();

        console.log("good");
        this.setState({orders: data, loading : false});
        console.log(data);

    }

    

    render() {


        let contents = this.state.loading ? 
            <></> :
            <table>
                <tr>
                    <th>Order Id</th>
                    <th>Buyer Id</th>
                    <th>Seller Id</th>
                    <th>Order Status</th>
                </tr>
                {this.state.orders.map((order, index) => (

                    <tr key={index }>
                        <td>{order.orderId}</td>
                        <td>{order.buyerId}</td>
                        <td>{order.sellerId}</td>
                        <td>{order.orderStatus}</td>
                    </tr>
                    
                ))}
            </table>

        return (
            <div className="main">
                <h1>React Search</h1>

                <div>

                    <label >Enter your user ID:</label>
                    <input type="text" id="fname" />
                    <button onClick={this.getOrders}>submit</button>

                </div>

                {contents}
                

            </div>
        );
    }
}
