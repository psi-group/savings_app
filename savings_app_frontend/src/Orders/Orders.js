import React, { Component, useState } from 'react';
import SearchAndDisplay from "../Search/SearchAndDisplay";

const Orders = () => {

    const [loading, setLoading] = useState(true);

    const getRole = (token) => {

    if (token != null)
        return JSON.parse(window.atob(token.split('.')[1]))["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    else
        return null;
    }

    const getId = (token) => {
    //console.log(JSON.parse(window.atob(token.split(".")[1])));
    return JSON.parse(window.atob(token.split(".")[1]))[
        "Id"];
    }


    const getOrders = async () => {

        const urlBasedOnRole = (getRole(localStorage.getItem("token")) == "seller") ?
            ("orderItems/seller/" + getId(localStorage.getItem("token"))) :
            "buyer/" + getId(localStorage.getItem("token"));

        fetch("https://localhost:7183/api/orders/" + urlBasedOnRole, {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.getItem("token")
            }
        }).then(async res => {
            console.log(await res.text());
        });

    }

    




        let contents = loading ? 
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
                <div>

                    <button onClick={getOrders}>GET YOUR ORDERS BUTTON</button>

                </div>

                {contents}
                

            </div>
        );
    }


export default Orders;