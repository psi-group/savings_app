import React, { Component } from "react";
import Filter from "./CategoriesFilter";
import { FilterDisplay } from "./CategoriesFilter";
import PriceFilter from "./Common/PriceFilter";
import "./App.css";
import SearchAndDisplay from "./Search/SearchAndDisplay";
import SortButton from "./Common/SortButton";

function Home(props) {
  const [content, setContent] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [sorting, setSorting] = React.useState("by_id");
  const [filters, setFilters] = React.useState([]);

  const renderForecastsTable = (products) => {
    return (
      <div className="main ml-20 mr-20 mt-10">
        <div>
          <div className="border-b-[1px] py-2 border-sky-500 flex gap-3">
            <SortButton setSorting={setSorting}></SortButton>
            <Filter filters={filters} setFilters={setFilters} />
            <PriceFilter />
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
