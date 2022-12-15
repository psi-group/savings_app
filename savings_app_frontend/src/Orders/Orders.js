import React, { Component, useState, useEffect } from 'react';
import SearchAndDisplay from "../Search/SearchAndDisplay";

const Orders = () => {

    const [loading, setLoading] = useState(true);
    const [orders, setOrders] = useState(null);
    const [errorMsg, setErrorMsg] = useState("");

    const getRole = (token) => {

    if (token != null)
        return JSON.parse(window.atob(token.split('.')[1]))["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
    else
        return null;
    }

    const getId = (token) => {
    return JSON.parse(window.atob(token.split(".")[1]))[
        "Id"];
    }

    useEffect(() => {
        fillOrders();
    }, [orders])

    const fillOrders = () => {
        if (orders == null) return;
        if (getRole(localStorage.getItem("token")) == "buyer") {
            const ordersFull = orders.map((order) => {
                delete order.buyerId;
                //delete order.id;
                order.orderItems.map((orderItem) => {
                    delete orderItem.id;
                    console.log("https://localhost:7183/api/pickups/" + orderItem.pickupId);
                    fetch("https://localhost:7183/api/pickups/" + orderItem.pickupId, {
                        method: "GET",
                        headers: {
                            "Access-Control-Allow-Origin": "*",
                            "Content-Type": "application/json",
                            "Authorization": "Bearer " + localStorage.getItem("token")
                        }
                    })
                        .then(async res => {

                            const json1 = await res.json();

                            if (!res.ok) {
                                return Promise.reject(json1);
                            }

                            orderItem.pickup = json1;

                            delete orderItem.pickup.id;
                            delete orderItem.pickup.productId;
                            delete orderItem.pickup.status;


                        }).catch(error => {
                            setErrorMsg(error.title);
                        })

                    delete orderItem.pickupId;
                    delete orderItem.orderId;
                    delete orderItem.orderItemStatus;
                })


                order.orderItems.map((orderItem) => {
                    delete orderItem.id;

                    console.log("https://localhost:7183/api/products/" + orderItem.productId);
                    fetch("https://localhost:7183/api/products/" + orderItem.productId, {
                        method: "GET",
                        headers: {
                            "Access-Control-Allow-Origin": "*",
                            "Content-Type": "application/json",
                            "Authorization": "Bearer " + localStorage.getItem("token")
                        }
                    })
                        .then(async res => {

                            const json1 = await res.json();

                            if (!res.ok) {
                                return Promise.reject(json1);
                            }

                            orderItem.product = json1;
                            if (orderItem.product.imageUrl == null) {
                                orderItem.product.imageUrl = "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg";
                            }

                            //delete orderItem.product.id;
                            orderItem.product.link = "https://localhost:3000/product/" + orderItem.product.id;
                            delete orderItem.product.amountOfUnits;
                            delete orderItem.product.amountPerUnit;
                            delete orderItem.product.price;


                        }).catch(error => {
                            setErrorMsg(error.title);
                        })

                    delete orderItem.productId;
                })
                
            });
        }
        else {
            const ordersFull = orders.map((orderItem) => {
                //delete order.buyerId;
                //delete order.id;

                    //delete orderItem.id;
                    console.log("https://localhost:7183/api/pickups/" + orderItem.pickupId);
                    fetch("https://localhost:7183/api/pickups/" + orderItem.pickupId, {
                        method: "GET",
                        headers: {
                            "Access-Control-Allow-Origin": "*",
                            "Content-Type": "application/json",
                            "Authorization": "Bearer " + localStorage.getItem("token")
                        }
                    })
                        .then(async res => {

                            const json1 = await res.json();

                            if (!res.ok) {
                                return Promise.reject(json1);
                            }

                            orderItem.pickup = json1;

                            //delete orderItem.pickup.id;
                            //delete orderItem.pickup.productId;
                            //delete orderItem.pickup.status;


                        }).catch(error => {
                            setErrorMsg(error.title);
                        })

                    //delete orderItem.pickupId;
                    //delete orderItem.orderId;
                    //delete orderItem.orderItemStatus;
                })


            orders.map((orderItem) => {
                //delete orderItem.id;

                console.log("https://localhost:7183/api/products/" + orderItem.productId);
                fetch("https://localhost:7183/api/products/" + orderItem.productId, {
                    method: "GET",
                    headers: {
                        "Access-Control-Allow-Origin": "*",
                        "Content-Type": "application/json",
                        "Authorization": "Bearer " + localStorage.getItem("token")
                    }
                })
                    .then(async res => {

                        const json1 = await res.json();

                        if (!res.ok) {
                            return Promise.reject(json1);
                        }

                        orderItem.product = json1;
                        if (orderItem.product.imageUrl == null) {
                            orderItem.product.imageUrl = "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg";
                        }

                        //delete orderItem.product.id;
                        orderItem.product.link = "https://localhost:3000/product/" + orderItem.product.id;
                        delete orderItem.product.amountOfUnits;
                        delete orderItem.product.amountPerUnit;
                        delete orderItem.product.price;


                    }).catch(error => {
                        setErrorMsg(error.title);
                    })

                //delete orderItem.productId;
            });
        }

        console.log(orders);
    }



    const getOrders = async () => {

        console.log("press");

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



            const json = await res.json();

            if (!res.ok) {
                return Promise.reject(json);
            }

            setOrders(json);

            

        })
            .catch(error => {
                setErrorMsg(error.title);

            })
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
                {orders.map((order, index) => (

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