import React, { useState } from "react";
import SearchBar from "./SearchBar.js";
import List from "../Product/ProductList";
import Filter from "../CategoriesFilter";
import { FilterDisplay } from "../CategoriesFilter";

const SearchAndDisplay = (props) => {

  return (
    <div>
      <List
        searched={props.searchas}
        products={props.products}
        filters={props.filters}
        sorting={props.sorting}
        isValidUrl={props.isValidUrl}
      />
    </div>
  );
};

export default SearchAndDisplay;
