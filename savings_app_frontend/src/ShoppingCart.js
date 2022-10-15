import React from "react";

export const ShoppingCart = (props) => {
  return (
    <div className="flex w-full h-full">
      <div className="bg-white w-2/3"></div>
      <div className="bg-sky-700 w-1/3 min-h-screen p-10 flex flex-col gap-3">
        <h1 className="text-2xl text-white">Summary</h1>
        <div className="bg-white h-1"></div>
        <div className="flex flex-col">
          <h3 className="text-lg text-white">Do You Have A Promo Code?</h3>
          <form className="flex">
            <input
              type="text"
              className="bg-transparent border-2 border-white text-white active:border-0 active:outline-none focus:outline-none"
            ></input>
            <button type="button" className="bg-white p-2">
              APPLY
            </button>
          </form>
        </div>
        <div className="bg-white h-1 mt-2"></div>
        {props.cartItems.length > 0 ? (
          <p>as</p>
        ) : (
          <h1>Your Shopping Cart Is Empty</h1>
        )}
      </div>
    </div>
  );
};

export default ShoppingCart;
