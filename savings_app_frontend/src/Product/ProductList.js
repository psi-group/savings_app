import React, { Component } from "react";
import { Link } from "react-router-dom";
import "./List.css";
import productImageSkeleton from "../img/productImageSkeleton.png";
import { CircleSpinnerOverlay } from "react-spinner-overlay";

export default class List extends Component {
  constructor(props) {
    super(props);
    this.state = { products: [], loading: true };
  }
  
  async populateProductsData() {

    let url =
      "https://localhost:7183/api/products/filter?search=" +
      this.props.searched +
      "&order=" +
      this.props.sorting;

    this.props.filters.map((filter) => {
      url = url + "&category=" + filter;
    });

    const response = await fetch(url);
    let data = await response.json();

    await this.setState({ products: data, loading: false });
    await console.log(data);
  }
  
  componentDidMount() {
    this.populateProductsData();
  }

  async componentDidUpdate(prevProps) {
    if (
      this.props.searched !== prevProps.searched ||
      this.props.filters != prevProps.filters ||
      this.props.sorting != prevProps.sorting
    ) {
      await this.populateProductsData();
    }
  }

  render() {
    return (
      <div>
        <ul>
        <div className="grid w-full grid-flow-row grid-cols-2 sm:grid-cols-3 md:grid-cols-3 lg:grid-cols-4 mt-10 gap-1 sm:gap-3">
            <CircleSpinnerOverlay
              loading={this.state.loading}
              color="#0ea5e9"
            />
            {this.state.products.map((product) => (
              <Link to={"/product/" + product.id}>
                <div className="flex align-middle justify-center rounded-xl  sm:min-w-[200px]">
                  <li
                    key={product.id}
                    className="font-bold text-white text-xl flex flex-col items-center gap-3 border-2 hover:border-sky-400 p-3 border-sky-600 rounded-md shadow-sm shadow-sky-600"
                  >
                    <h1 className="font-extrabold text-transparent text-xl bg-clip-text bg-gradient-to-r from-sky-400 to-sky-700">
                      {product.name.toUpperCase()}
                    </h1>
                    <img
                      src={
                        "https://savingsapp.blob.core.windows.net/productimages/" +
                        product.id +
                        ".jpg"
                          ? "https://savingsapp.blob.core.windows.net/productimages/" +
                            product.id +
                            ".jpg"
                          : productImageSkeleton
                      }
                      className="w-60 h-60 rounded-md"
                    />
                    <div className="flex justify-between text-base flex-col items-center">
                    <p className="text-sky-600">
                        {product.category}
                      </p>
                      <p className="text-sky-600">
                        {product.price} Eur / {product.amountPerUnit}{" "}
                        {product.amountType}
                      </p>
                      <p className="text-sky-600 text-xs">
                        Max Quantity: {product.amountOfUnits}{" "}
                        {product.amountType}
                      </p>
                      
                    </div>
                  </li>
                </div>
              </Link>
            ))}
          </div>
        </ul>
      </div>
    );
  }
}
