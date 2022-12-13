import React, { Component } from "react";
import Filter from "./CategoriesFilter";
import { FilterDisplay } from "./CategoriesFilter";
import PriceFilter from "./Common/PriceFilter";
import "./App.css";
import SearchAndDisplay from "./Search/SearchAndDisplay";
import SortButton from "./Common/SortButton";
import { Link } from "react-router-dom";
import MaxResults from "./Common/MaxResults";

function Home(props) {
  const [content, setContent] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [sorting, setSorting] = React.useState("by_id");
  const [filters, setFilters] = React.useState([]);

    console.log("bandom");
    console.log(props);
    console.log(props.isValidUrl("hello"));

  const renderForecastsTable = (products) => {
    return (
      <div className="main mx-1 md:!mx-auto mt-10 max-w-[700px] lg:max-w-[900px]">
        <h1 className="text-4xl mb-4 text-sky-500">Products</h1>
        <div>
          <div className="border-y-[1px] py-2 border-sky-500  items-center flex md:gap-3 pl-4">
            <SortButton setSorting={setSorting}></SortButton>
            <Filter filters={filters} setFilters={setFilters} />
             {/* <PriceFilter />  */}
             <MaxResults />
            <Link to="/restaurants" className="border-sky-500 border-1 rounded-md px-4 p-1 outline-none hover:bg-sky-100 hidden md:block">Switch to restaurants</Link>
          </div>
          <FilterDisplay filters={filters} setFilters={setFilters} />

          <SearchAndDisplay
            searchas={props.searchas}
            products={products}
            sorting={sorting}
            isValidUrl={props.isValidUrl}
            filters={filters}
            setFilters={setFilters}
          />
        </div>
      </div>
    );
  };

  React.useEffect(() => {
    console.log("useEffect");
    fetch("https://localhost:7183/api/products/")
      .then((res) => res.json())
      .then((res) => {
        setContent(res);
        setLoading(false);
      })
      .catch((err) => console.log(err));
  }, []);

  let contents = loading ? (
    <p>
      <em>
        Loading... Please refresh once the ASP.NET backend has started. See{" "}
        <a href="https://aka.ms/jspsintegrationreact">
          https://aka.ms/jspsintegrationreact
        </a>{" "}
        for more details.
      </em>
    </p>
  ) : (
    renderForecastsTable(content)
  );

  return <div>{renderForecastsTable(content)}</div>;

}


export default Home;