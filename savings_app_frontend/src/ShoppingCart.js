import React from "react";
import { Link } from "react-router-dom";

export const ShoppingCart = (props) => {
  return (
    <div className="flex w-full h-full">
      <div className="bg-white w-2/3 p-16 flex flex-col">
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
                    src={cartItem.image}
                    className="w-24 h-20 rounded-md border-zinc-500 border-2"
                  />
                  <div className="flex flex-col">
                    <h1 className="font-bold italic text-xl tracking-wide">
                      {cartItem.itemName}
                    </h1>
                    <div className="flex gap-1 items-center">
                      <h3 className="font-bold text-md">Pickup Time: </h3>
                      <p className="text-sm">{cartItem.pickupTime.startTime} to {cartItem.pickupTime.endTime}</p>
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
                <p className="font-bold self-center">{cartItem.fullPrice} Eur</p>
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
        </div>
      </div>
      <div className="bg-sky-700 w-1/3 min-h-screen p-10 pt-24 flex flex-col gap-3">
        <h1 className="text-2xl text-white">Summary</h1>
        <div className="bg-white h-1"></div>
        <div className="flex flex-col">
          <h3 className="text-lg text-white">Do You Have A Promo Code?</h3>
          <form className="flex">
            <input
              type="text"
              className="bg-transparent border-2 border-white text-white active:border-0 active:outline-none focus:outline-none"
            ></input>
            <button type="button" className="bg-white p-2 font-bold">
              APPLY
            </button>
          </form>
        </div>
        <div className="bg-white h-1 mt-2"></div>
        <div className="flex justify-between justify-self-end items-center">
          <h1 className="text-white text-2xl ">Total </h1>
          <h1 className="font-bold text-white text-2xl">{props.fullSum} Eur</h1>
        </div>
        <div className="bg-white h-1 mt-2"></div>
        <button
          type="button"
          className="bg-white p-2 pt-2 font-bold justify-self-end "
        >
          Checkout
        </button>
      </div>
    </div>
  );
};

export default ShoppingCart;
