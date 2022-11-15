import React from "react";

import searchIcon from "../img/searchIcon.png";

export const Searchbar = (props) => {
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
    <div className="flex items-center rounded-xl bg-slate-300 border-1 px-3 py-1">
      <select
        id="selector"
        className="text-center border-r-4 h-full border-transparent font-bold focus:outline-none outline-none bg-slate-300"
        onChange={setSelect}
      >
        <option>Products</option>
        <option>Restaurants</option>
      </select>
      <input
        className="bg-transparent focus:outline-none w-full"
        type="text"
        id="search"
        placeholder="Search"
      />
      <img src={searchIcon} className="w-5 h-5" onClick={setSearch} />
    </div>
  );
};
