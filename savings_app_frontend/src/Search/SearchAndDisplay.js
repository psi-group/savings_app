import React, { useState } from "react";
import SearchBar from "./SearchBar.js";
import List from "../Product/ProductList";
import Filter from "../CategoriesFilter";
import { FilterDisplay } from "../CategoriesFilter";

const SearchAndDisplay = (props) => {
  const [filters, setFilters] = useState([]);

  return (
    <div>
      <Filter filters={filters} setFilters={setFilters} />
      <FilterDisplay filters={filters} setFilters={setFilters} />
      <List
        searched={props.searchas}
        products={props.products}
        filters={filters}
        sorting={props.sorting }
        isValidUrl={props.isValidUrl}
      />
    </div>
  );
};

export default SearchAndDisplay;
