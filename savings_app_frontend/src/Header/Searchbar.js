import React, { useEffect, useRef } from "react";
import { useLocation } from "react-router-dom";

import searchIcon from "../img/searchIcon.png";

export const Searchbar = (props) => {
  const location = useLocation();
  const searchInputRef = useRef(null);

  useEffect(() => {
    searchInputRef.current.value = "";
  }, [location]);

  function setSearch() {
    let value = document.getElementById("search").value;
    props.setSearchas(value);

    var selected = document.getElementById("selector");
    var text = selected.options[selected.selectedIndex].text;
    var currentUrl =
      window.location.href === "https://localhost:3000/"
        ? window.location.href + "products"
        : window.location.href;

    if (currentUrl !== "https://localhost:3000/" + text.toLowerCase()) {
      props.navigate(text.toLowerCase());
    }
  }

  function setSelect() {
    var selected = document.getElementById("selector");
    var text = selected.options[selected.selectedIndex].text;
    props.setSelector(text);
  }
  return (
    <div className="flex items-center rounded-xl bg-white border-1 border-sky-500 px-3 py-2 w-[50%]">
      <select
        id="selector"
        className="text-center text-sky-500 border-r-4 h-full border-transparent font-bold focus:outline-none outline-none"
        onChange={setSelect}
      >
        <option>Products</option>
        <option>Restaurants</option>
      </select>
      <input
        className="bg-transparent focus:outline-none w-full text-black"
        type="text"
        id="search"
        placeholder="Search"
        ref={searchInputRef}
      />
      <img src={searchIcon} className="w-5 h-5" onClick={setSearch} />
    </div>
  );
};
