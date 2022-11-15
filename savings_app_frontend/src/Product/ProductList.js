import React, { Component } from "react";
import { Link } from "react-router-dom";
import "./List.css";
import productImageSkeleton from "../img/productImageSkeleton.png";

export default class List extends Component {
  constructor(props) {
    super(props);
    this.state = { products: [] };
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
        <h4 className="text-sky-800 font-bold text-xl mt-3">
          Searching for: {this.props.searched}
        </h4>
        <ul>
          <div className="grid w-full grid-flow-row-dense grid-cols-4  mt-16 px-32 gap-10">
            {this.state.products.map((product) => (
              <Link to={"/product/" + product.id}>
                <div className="flex align-middle justify-center rounded-xl">
                  <li
                    key={product.id}
                    className="font-bold text-white text-xl flex flex-col items-center gap-3 border-2 hover:border-sky-400 p-3 border-sky-600 rounded-md shadow-sm shadow-sky-600"
                  >
                    <h1 className="font-extrabold text-transparent text-xl bg-clip-text bg-gradient-to-r from-sky-400 to-sky-700">
                      {product.name.toUpperCase()}
                    </h1>
                    <img
                      src={
                        this.props.isValidUrl(product.pictureURL)
                          ? product.pictureURL
                          : productImageSkeleton
                      }
                      className="w-72 h-72 rounded-md"
                    />
                    <div className="flex justify-between text-base flex-col items-center">
                      <p className="text-sky-600">{product.price} Eur / {product.amountPerUnit} {product.amountType}</p>
                      <p className="text-sky-600 text-xs">Max Quantity: {product.amountOfUnits} {product.amountType}</p>
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
