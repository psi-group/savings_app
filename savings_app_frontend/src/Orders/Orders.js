import React, { useState, useEffect } from "react";
import UserLinks from "../Common/UserLinks";
import { Link } from "react-router-dom";
import { CircleSpinnerOverlay } from "react-spinner-overlay";
import OrderSortButton from "../Common/OrderSortButton";

const Orders = () => {
  const [loading, setLoading] = useState(true);
  const [orders, setOrders] = useState(null);
  const [errorMsg, setErrorMsg] = useState("");
console.log(orders);
  const getRole = (token) => {
    if (token != null)
      return JSON.parse(window.atob(token.split(".")[1]))[
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
      ];
    else return null;
  };

  const getId = (token) => {
    return JSON.parse(window.atob(token.split(".")[1]))["Id"];
  };
  
  useEffect(() => {
    getOrders();
    console.log(orders);
  }, [])


  
  console.log(orders);
  const getOrders = async () => {

    const urlBasedOnRole =
      getRole(localStorage.getItem("token")) == "seller"
        ? "orderItems/seller/" + getId(localStorage.getItem("token"))
        : "buyer/" + getId(localStorage.getItem("token"));

    fetch("https://localhost:7183/api/orders/" + urlBasedOnRole, {
      method: "GET",
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then(async (res) => {
        const json = await res.json();

        if (!res.ok) {
          return Promise.reject(json);
        }
        setLoading(false);
        setOrders(json);
      })
      .catch((error) => {
        setErrorMsg(error.title);
      });
  };

  let contents = loading ? (
    <></>
  ) : (
    <table>
      <tr>
        <th>Order Id</th>
        <th>Buyer Id</th>
        <th>Seller Id</th>
        <th>Order Status</th>
      </tr>
      {orders.map((order, index) => (
        <tr key={index}>
          <td>{order.orderId}</td>
          <td>{order.buyerId}</td>
          <td>{order.sellerId}</td>
          <td>{order.orderStatus}</td>
        </tr>
      ))}
    </table>
  );
  const getFullDate = (fullDate) => {
    const date = new Date(fullDate);
    const minutes = date.getMinutes() < 10 ? `0${date.getMinutes()}` : date.getMinutes();
    return `${date.getFullYear()}-${date.getMonth()+1}-${date.getDate()}   ${date.getHours()}:${minutes }`;
  };

  const getFullPrice = (orderItems) => {
    let sum = 0;
    orderItems.map(orderItem => {
        sum += orderItem.price * orderItem.unitsOrdered;
    })
    return `${sum} Eur`;

  }

  return !loading ? (
    <div className="flex justify-center  mt-40 gap-3 ">
        <UserLinks />
        <div className="w-full flex flex-col gap-3 max-w-[780px]">
            <h1 className="text-4xl">My Orders</h1>
            
        <div className="bg-slate-100 w-full rounded-xl min-h-[400px] border-2 border-sky-500 pt-5 px-5 flex-col flex justify-between py-5 ">
        <OrderSortButton />
            <div>
            <table className="w-full bg-sky-500 text-white h-[50px] rounded-t-xl flex justify-between items-center px-5">
                <div className="flex gap-[100px]">
                <th>Order date</th>
                <th>Ordered items</th>
                </div>
                <th className="justify-self-end">Price</th>
            </table>
            
            {orders !== null && orders.map(order => (
                <Link to={`/orders/${order.id}`}>
                <div className="w-full bg-white px-5 h-[50px] flex items-center hover:!bg-slate-200 hover:!text-black justify-between">
                <div className="flex gap-[100px]" >
                <div>{getFullDate(order.orderDate)}</div>
                <div>{order.orderItems.length}</div>
                </div>
                <div className="justify-self-end">{getFullPrice(order.orderItems)}</div>
                </div>
                </Link>
            ))}
            {orders ===null && (
                <div className="w-full bg-white flex items-center justify-center h-[50px]"><h2 className="text-2xl text-sky-500" >You do not have active orders</h2></div>
            )}
            </div>
            <div className=" flex flex-col w-full gap-2">
                <hr className="bg-black"></hr>
                <div className="flex justify-between w-full">
                <h1 className="text-2xl mt-3">Need more products?</h1>
                <Link to="/" className="text-xl bg-sky-500 p-2 rounded-md text-white hover:bg-sky-600 mt-3">Go back to shopping</Link> </div>
                </div>
        </div>
        </div>
    </div>
  ): <CircleSpinnerOverlay
  loading={loading}
  color="#0ea5e9"
/>;
};

export default Orders;
