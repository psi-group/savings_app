import React, { useState } from "react";
import "./Filter.css";
import DropdownMenu from "./DropdownMenu";
import { CATEGORIES } from "../Constants";

const CategoriesFilter = (props) => {
  const [open, setOpen] = useState(false);

  return (
    <>
      <div class="relative">
        <button
          type="button"
          className={open ? "p-1 px-4 italic bg-sky-500 rounded-md text-white border-sky-500 border-[1px]" : "p-1 px-4 bg-white italic rounded-md border-[1px] border-sky-500 hover:!bg-sky-100"}
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
