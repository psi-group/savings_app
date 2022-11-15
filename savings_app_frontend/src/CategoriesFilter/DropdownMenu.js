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
      className="dropdown"
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
          <div className="menu">
            <div className="menu-item goBack" goToMenu="main">
              <div
                className="goBackButton"
                onClick={() => setActiveMenu("main")}
              >
                <img src={arrow} className="flippedArrow" />
              </div>
              <h3>{category.category.toUpperCase()}</h3>
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
