import React, { useState } from "react";
import { Link } from "react-router-dom";

export const ShoppingCart = (props) => {
  const [isSelectTimeOpen, setIsSelectTimeOpen] = useState(false);
    const [isPathFinderReady, setIsPathFinderReady] = useState(null);

    console.log(props.cartItems);

  const handleFormSubmit = (e) => {
    e.preventDefault();
    console.log(e.target[0].value);
    console.log(e.target[1].value);
    setIsSelectTimeOpen(false);
    };

    const getId = (token) => {
        //console.log(JSON.parse(window.atob(token.split(".")[1])));
        return JSON.parse(window.atob(token.split(".")[1]))[
            "Id"
        ];
    }
    

    const handlePlanVisits = (e) => {
        e.preventDefault();
        console.log(e.target[0].value);
        console.log(e.target[1].value);

        setIsPathFinderReady(false);

        console.log("ready? " + isPathFinderReady);

        fetch("https://localhost:7183/api/pathfinder", {
            method: "GET",

        })
            .then(async res => {
                console.log(await res.json());
                console.log("ready? " + isPathFinderReady);

                setIsPathFinderReady(true);
                console.log("ready? " + isPathFinderReady);
            })
    }

  const handleCheckout = () => {
      console.log(props.cartItems);
      
      const productsToBuy = [];
      props.cartItems.map(
          (cartItem) => {
              productsToBuy.push({
                  id: cartItem.product.id,
                  pickupId: cartItem.pickupTime.id,
                  amount: cartItem.quantity
              })
          }
      );

      console.log(JSON.stringify({
          productsToBuy: productsToBuy,
          buyerId: getId(localStorage.getItem("token"))
      }));

      console.log(productsToBuy);
      fetch("https://localhost:7183/api/shop/checkout", {
          method: "POST",
          headers: {
              "Content-Type": "application/json",
          },
          body: JSON.stringify({
              productsToBuy: productsToBuy,
              buyerId: getId(localStorage.getItem("token"))
          }),
      });

      return;

    props.cartItems.map((cartItem) => {
      fetch("https://localhost:7183/api/pickups/" + cartItem.pickupTime.id, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          id: cartItem.pickupTime.id,
          productId: cartItem.pickupTime.productId,
          startTime: cartItem.pickupTime.startTime,
          endTime: cartItem.pickupTime.endTime,
          status: "taken",
        }),
      }).then((res) => {
        let body = JSON.stringify({
          status: "awaitingPickup",
          sellerId: cartItem.restaurant.id,
          BuyerId: "a2e5346e-b246-4578-b5cd-993af7f77d11",
          PickupId: cartItem.pickupTime.id,
          productId: cartItem.product.id,
        });
        console.log(body);

        fetch("https://localhost:7183/api/orders", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            status: "awaitingPickup",
            sellerId: cartItem.restaurant.id,
            BuyerId: "a2e5346e-b246-4578-b5cd-993af7f77d11",
            PickupId: cartItem.pickupTime.id,
            productId: cartItem.product.id,
          }),
        }).then((res) => {
          console.log(
            "fetchinam produkuts " +
              "https://localhost:7183/api/products/" +
              cartItem.product.id
          );
          fetch("https://localhost:7183/api/products/" + cartItem.product.id, {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              id: cartItem.product.id,
              name: cartItem.product.name,
              category: cartItem.product.category,
              restaurantId: cartItem.product.restaurantID,
              amountType: cartItem.product.amountType,
              amountPerUnit: cartItem.product.amountPerUnit,
              amountOfUnits:
                cartItem.product.amountOfUnits - cartItem.unitQuantity,
              price: cartItem.product.price,
              pictureUrl: cartItem.product.pictureUrl,
              shelfLife: cartItem.product.shelfLife,
              description: cartItem.product.description,
            }),
          });
        });
      });
    });

    props.setCartItems([]);
    props.setFullSum(0);
  };

  return (
    <>
      <div className="flex w-full h-full">
        <div className="bg-white w-2/3 p-16 flex flex-col relative">
          <h1 className="text-2xl font-bold">MY SHOPPING CART</h1>
          <div className="bg-zinc-700 h-1"></div>
          <div className="flex justify-between pt-5">
            <p className="text-zinc-500">PRODUCT</p>
            <div className="flex gap-20 text-zinc-500">
              <p className="font-bol">TOTAL</p>
            </div>
          </div>
          <div className="bg-zinc-700 h-0.5 mb-3"></div>
          <div className="flex flex-col gap-4">
            {props.cartItems.length > 0 ? (
              props.cartItems.map((cartItem, index) => {
                return (
                  <>
                    <div className="flex justify-between border-b-2 border-zinc-500 pb-2">
                      <div className="flex gap-5">
                                <img
                                    src={"https://localhost:7183/productImg/" + cartItem.product.id + ".jpg"}
                          className="w-24 h-20 rounded-md border-zinc-500 border-2"
                        />
                        <div className="flex flex-col">
                          <h1 className="font-bold italic text-xl tracking-wide">
                            {cartItem.itemName}
                          </h1>
                          <div className="flex gap-1 items-center">
                            <h3 className="font-bold text-md">Pickup Time: </h3>
                            <p className="text-sm">
                              {cartItem.pickupTime.startTime} to{" "}
                              {cartItem.pickupTime.endTime}
                            </p>
                          </div>
                          <div className="flex gap-1 items-center">
                            <h3 className="font-bold text-md">Quantity: </h3>
                            <p className="text-sm">
                              {cartItem.quantity} {cartItem.quantityType}
                            </p>
                          </div>
                          <button
                            className="self-start text-sm text-zinc-500"
                            onClick={() => props.removeCartItem(index)}
                          >
                            Remove
                          </button>
                        </div>
                      </div>
                      <div className="bg-zinc-500 h-0.5 my-3"></div>
                      <p className="font-bold self-center">
                        {cartItem.fullPrice} Eur
                      </p>
                    </div>
                  </>
                );
              })
            ) : (
              <div className="flex flex-col items-center gap-3">
                <h1 className="text-center text-black font-bold pt-10 text-xl">
                  Your Shopping Cart Is Empty
                </h1>
                <Link to="/">
                  <button className="px-5 py-2 bg-black hover:font-bold hover:text-white text-lg">
                    SHOP
                  </button>
                </Link>
              </div>
            )}
            {isSelectTimeOpen && (
              <div className="m-auto bg-white border-4 w-2/5 h-56 rounded-lg border-sky-500 shadow-lg relative">
                <button className="text-xl absolute top-1 right-3" onClick={() => setIsSelectTimeOpen(false)}>X</button>
                <form
                  className="flex justify-center items-center h-full flex-col gap-2"
                                  onSubmit={handlePlanVisits}
                >
                  <h1 className="font-bold text-sky-600 text-xl">
                    Select time
                  </h1>
                  <div className="flex gap-3">
                    <input
                      type="time"
                      id="time1"
                      name="time1"
                      defaultValue="00:00"
                    ></input>
                    <h1 className="font-bold text-sky-900 text-xl">-</h1>
                    <input
                      type="time"
                      placeholder="12:00"
                      name="time2"
                      id="time2"
                      defaultValue="12:00"
                    ></input>
                  </div>
                  <button
                                      type="submit"
                                      className="bg-sky-700 active:text-black text-white  border-2 active:border-black hover:bg-sky-800 p-2 pt-2 font-bold self-center w-[50%]"
                                      
                  >
                                      Confirm
                  </button>
                              </form>
                              <div>
                                  {(!isPathFinderReady && isPathFinderReady != null) ? (<div id="load"><p>Loading....</p></div>) : (<></>)}
                                  </div>
              </div>
            )}
          </div>
        </div>
        <div className="bg-sky-700 w-1/3 min-h-screen p-10 pt-24 flex flex-col justify-between">
          <div>
          <h1 className="text-2xl text-white">Summary</h1>
          <div className="bg-white h-1 mt-2"></div>
          </div>
          <div className="pb-24 flex flex-col gap-2">

          
          <div className="flex justify-between justify-self-end items-center">
            <h1 className="text-white text-2xl ">Total </h1>
            <h1 className="font-bold text-white text-2xl">
              {props.fullSum} Eur
            </h1>
          </div>
          <div className="bg-white h-1 mb-2"></div>
          
          <button
            type="button"
            className="bg-white p-2 pt-2 font-bold justify-self-end mb-2"
            onClick={handleCheckout}
          >
            Checkout
          </button>
          <button
            type="button"
            className="bg-sky-900 active:text-black text-white  border-2 active:border-black hover:bg-sky-800 p-2 pt-2 font-bold self-center w-[50%]"
            onClick={() => setIsSelectTimeOpen(!isSelectTimeOpen)}
          >
            Calculate pickup times
          </button>
          </div>
        </div>
      </div>
    </>
  );
};

export default ShoppingCart;
