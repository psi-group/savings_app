import React, { useEffect, useRef, useState } from "react";
import DropdownItem from "./DropdownItem";
import { CSSTransition } from "react-transition-group";
import "./Filter.css";

const DropdownMenu = ({ categories, setFilters, filters }) => {
  const [activeMenu, setActiveMenu] = useState("main");
  const [menuHeight, setMenuHeight] = useState(null);
  const dropdownRef = useRef(null);

  const arrow =
    window.location.protocol +
    "//" +
    window.location.host +
    "/Images/arrow.png";

  useEffect(() => {
    setMenuHeight(dropdownRef.current?.firstChild.offsetHeight);
  }, []);

  function calcHeight(el) {
    const height = el.offsetHeight;
    setMenuHeight(height);
  }

  return (
    <div
      className="absolute z-10 bg-sky-500 w-[250px]  block left-0 p-1 border-[1px] border-sky-500 overflow-hidden rounded-xl transition:[height] duration-200 ease-in"
      style={{ height: menuHeight + 25 }}
      ref={dropdownRef}
    >
      <CSSTransition
        in={activeMenu === "main"}
        unmountOnExit
        timeout={500}
        classNames="menu-primary"
        onEnter={calcHeight}
      >
        <div className="menu">
          {categories.map((category, index) => (
            <DropdownItem
              key={index}
              goToMenu={category.category}
              activeMenu={activeMenu}
              setActiveMenu={setActiveMenu}
              arrow={arrow}
              filters={filters}
              setFilters={setFilters}
            >
              {category.category.toUpperCase()}
            </DropdownItem>
          ))}
        </div>
      </CSSTransition>
      {categories.map((category) => (
        <CSSTransition
          in={activeMenu === category.category}
          unmountOnExit
          timeout={500}
          classNames="menu-secondary"
          onEnter={calcHeight}
        >
          <div className="w-full z-1">
            <div className="h-[50px] transition-[background] duration-300 padding-1 rounded-md  relative flex justify-center items-center self-center text-white" goToMenu="main">
              <div
                className="absolute ml-[5px] bottom-[12px] left-0 w-[30px] h-[30px] flex justify-center items-center"
                onClick={() => setActiveMenu("main")}
              >
                <img src={arrow} className="-scale-x-100 !h-[30px] !w-[30px] hue-rotate-[50%] hover:bg-white rounded-lg" />
              </div>
              <h3 className="font-bold text-[30px]">{category.category.toUpperCase()}</h3>
            </div>
            {category.subCategories &&
              category.subCategories.map((sub) => {
                return (
                  <DropdownItem
                    key={sub}
                    img={arrow}
                    filters={filters}
                    setFilters={setFilters}
                  >
                    {sub.toUpperCase()}
                  </DropdownItem>
                );
              })}
          </div>
        </CSSTransition>
      ))}
    </div>
  );
};

export default DropdownMenu;
