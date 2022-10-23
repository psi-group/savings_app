import React, { useState } from "react";
import "./Filter.css";
import DropdownMenu from "./DropdownMenu";
import { CATEGORIES } from "../Constants";

const CategoriesFilter = (props) => {
  const [open, setOpen] = useState(false);

  return (
    <>
      <div class="filterContainer">
        <button
          type="button"
          className={open ? "dropdownButtonBlack" : "dropdownButton"}
          onClick={() => setOpen(!open)}
        >
          Categories
        </button>
        {open && CATEGORIES && (
          <DropdownMenu
            categories={CATEGORIES}
            setFilters={props.setFilters}
            filters={props.filters}
          />
        )}
      </div>
    </>
  );
};

export default CategoriesFilter;
