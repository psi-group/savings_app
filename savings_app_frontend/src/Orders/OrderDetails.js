import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Link } from "react-router-dom";
import UserLinks from "../Common/UserLinks";
import { CircleSpinnerOverlay } from "react-spinner-overlay";

const OrderDetails = () => {
  const [loading, setLoading] = useState(true);
  const [orders, setOrders] = useState(null);
  const [productInfo, setProductInfo] = useState([]);
  const params = useParams();

  useEffect(() => {
    getOrders();
  }, []);
  useEffect(() => {
    fillOrders();
  }, [orders]);

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
    }).then(async (res) => {
      const json = await res.json();

      if (!res.ok) {
        return Promise.reject(json);
      }
      setOrders(json);
      fillOrders();
    });
  };

  const fillOrders = () => {
    if (orders === null) return;
    if (getRole(localStorage.getItem("token")) == "buyer") {
      const ordersFull = orders.map((order) => {
        delete order.buyerId;
        //delete order.id;
        order.orderItems.map((orderItem) => {
          delete orderItem.id;

          fetch("https://localhost:7183/api/pickups/" + orderItem.pickupId, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "*",
              "Content-Type": "application/json",
              Authorization: "Bearer " + localStorage.getItem("token"),
            },
          })
            .then(async (res) => {
              const json1 = await res.json();
              setLoading(false);

              if (!res.ok) {
                return Promise.reject(json1);
              }

              orderItem.pickup = json1;

              delete orderItem.pickup.id;
              delete orderItem.pickup.productId;
              delete orderItem.pickup.status;
            })
            .catch((error) => {});

          delete orderItem.pickupId;
          delete orderItem.orderId;
          delete orderItem.orderItemStatus;
        });

        order.orderItems.map((orderItem) => {
          delete orderItem.id;

          fetch("https://localhost:7183/api/products/" + orderItem.productId, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "*",
              "Content-Type": "application/json",
              Authorization: "Bearer " + localStorage.getItem("token"),
            },
          })
            .then(async (res) => {
              const json1 = await res.json();
              setProductInfo([...productInfo, json1]);

              if (!res.ok) {
                return Promise.reject(json1);
              }
              setLoading(false);

              orderItem.product = json1;
              if (orderItem.product.imageUrl == null) {
                orderItem.product.imageUrl =
                  "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg";
              }

              //delete orderItem.product.id;
              orderItem.product.link =
                "https://localhost:3000/product/" + orderItem.product.id;
              delete orderItem.product.amountOfUnits;
              delete orderItem.product.amountPerUnit;
              delete orderItem.product.price;
            })
            .catch((error) => {});

          delete orderItem.productId;
        });
      });
    } else {
      const ordersFull = orders.map((orderItem) => {
        //delete order.buyerId;
        //delete order.id;

        //delete orderItem.id;

        fetch("https://localhost:7183/api/pickups/" + orderItem.pickupId, {
          method: "GET",
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("token"),
          },
        })
          .then(async (res) => {
            const json1 = await res.json();
            setLoading(false);

            if (!res.ok) {
              return Promise.reject(json1);
            }
            orderItem.pickup = json1;

            //delete orderItem.pickup.id;
            //delete orderItem.pickup.productId;
            //delete orderItem.pickup.status;
          })
          .catch((error) => {});

        //delete orderItem.pickupId;
        //delete orderItem.orderId;
        //delete orderItem.orderItemStatus;
      });

      orders.map((orderItem) => {
        fetch("https://localhost:7183/api/products/" + orderItem.productId, {
          method: "GET",
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "application/json",
            Authorization: "Bearer " + localStorage.getItem("token"),
          },
        })
          .then(async (res) => {
            const json1 = await res.json();
            if (!res.ok) {
              return Promise.reject(json1);
            }
            setLoading(false);
            orderItem.product = json1;
            if (orderItem.product.imageUrl == null) {
              orderItem.product.imageUrl =
                "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg";
            }

            //delete orderItem.product.id;
            orderItem.product.link =
              "https://localhost:3000/product/" + orderItem.product.id;
            delete orderItem.product.amountOfUnits;
            delete orderItem.product.amountPerUnit;
            delete orderItem.product.price;
          })
          .catch((error) => {});

        //delete orderItem.productId;
      });
    }
  };
  const getFullDate = (fullDate) => {
    const date = new Date(fullDate);
    return `${date.getFullYear()}-${date.getMonth()}-${date.getDay()}   ${date.getHours()}:${date.getMinutes()}`;
  };

  const getFullPrice = (orderItems) => {
    let sum = 0;
    orderItems.map(orderItem => {
        sum += orderItem.price * orderItem.unitsOrdered;
    })
    return `${sum} Eur`;

  }

  const order =
    orders &&
    orders.filter(
      (order) => order.id === params.id && order.orderItems !== undefined
    )[0];

  return !loading ? (
    <div className="flex justify-center  mt-40 gap-3 ">
      <UserLinks />
      <div className="w-full flex flex-col gap-3 max-w-[780px]">
        <div className="flex gap-1 items-center">
          <h1 className="text-4xl">Order details: </h1>
          <p className="text-lg self-end">{params.id}</p>
        </div>
        <div className="bg-slate-100 w-full rounded-xl min-h-[400px] border-2 border-sky-500 pt-3 px-3 flex-col flex  py-3 ">
          <Link to="/orders" className="bg-sky-500 text-white p-1 w-[60px] rounded-xl hover:bg-sky-600 text-center" >Go back</Link>
          <div className="flex gap-1">
            <p className="text-xl font-normal">Order date:</p>
            <p className="text-xl font-bold text-sky-500">
              {getFullDate(order.orderDate)}
            </p>
          </div>
          <hr className="bg-black my-3"></hr>
          <h2 className="text-2xl">Ordered items</h2>
          <div className="w-full bg-sky-500 text-white h-[50px] rounded-t-xl flex justify-between px-3 items-center text-lg">
            <p>Product</p>
            <div className="flex gap-5">
              <p>Quantity</p>
              <p>Price</p>
            </div>
          </div>
          {order.orderItems &&
            order.orderItems.map((orderItem) => (
              <div className={"bg-white h-[100px] w-full  flex justify-between px-3" }>
                <div className="flex gap-3 items-center">
                  <img
                    src={orderItem.product?.imageUrl}
                    className="w-[60px] h-[60px]"
                  />
                  <div>
                    <p className="text-xl text-sky-500">
                      {orderItem.product?.name}
                    </p>
                    <p>Pickup time</p>
                    <p>
                      From {getFullDate(orderItem.pickup?.startTime)} to{" "}
                      {getFullDate(orderItem.pickup?.endTime)}
                    </p>
                  </div>
                </div>
                <div className="flex gap-20 items-center">
                  <p>{orderItem.unitsOrdered}</p>
                  <p>{orderItem.price} Eur</p>
                </div>
              </div>
            ))}
            <div className="h-[50px] w-full bg-sky-500 mt-4 rounded-xl flex justify-between px-3 items-center text-white text-xl">
              <p>Full price: </p>
              <p>{getFullPrice(order.orderItems)}</p>
            </div>
        </div>
      </div>
    </div>
  ) : (
    <CircleSpinnerOverlay loading={loading} color="#0ea5e9" />
  );
};

export default OrderDetails;
