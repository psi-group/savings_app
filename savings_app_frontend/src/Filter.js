import React, { useEffect, useRef, useState } from 'react';
import { CSSTransition } from 'react-transition-group';
import "./Filter.css";
import arrow from './img/arrow.png';
import "./useFetchData.js";
import useFetchData from './useFetchData.js';



const Filter = (props) => {

    const [categories] = useFetchData("https://localhost:7183/api/categories");
    const [open, setOpen] = useState(false);

    return (

        <>
            <div class="filterContainer">
                <button type="button" onClick={() => setOpen(!open)}>Categories</button>
                {open && categories &&
                    <DropdownMenu categories={categories} />
                }
            </div>
        </>

    );
};

const DropdownMenu = ({ categories }) => {
    const [activeMenu, setActiveMenu] = useState('main');
    const [menuHeight, setMenuHeight] = useState(null);
    const dropdownRef = useRef(null);

    useEffect(() => {
        setMenuHeight(dropdownRef.current?.firstChild.offsetHeight)
    }, [])

    function calcHeight(el) {
        const height = el.offsetHeight;
        setMenuHeight(height);
    }

    const DropdownItem = (props) => {
        return (
            <a href="#" className="menu-item" onClick={() => props.goToMenu && setActiveMenu(props.goToMenu)}>
                {props.children}
                {activeMenu == 'main' &&
                    <img src={arrow} />
                }
            </a>

        );
    }

    return (
        <div className="dropdown" style={{ height: menuHeight + 25 }} ref={dropdownRef}>

            <CSSTransition
                in={activeMenu === 'main'}
                unmountOnExit
                timeout={500}
                classNames="menu-primary"
                onEnter={calcHeight}
            >
                <div className="menu">
                    {categories.map((category, index) =>

                        <DropdownItem key={index} goToMenu={category.categoryName}>{category.categoryName}</DropdownItem>
                    )
                    }

                </div>
            </CSSTransition>


            {categories.map(category =>
                <CSSTransition
                    in={activeMenu === category.categoryName}
                    unmountOnExit
                    timeout={500}
                    classNames="menu-secondary"
                    onEnter={calcHeight}
                >
                    <div className="menu">
                        <DropdownItem goToMenu="main">Go back</DropdownItem>
                        {category.subcategories.map(sub => {
                            return <DropdownItem key={sub} img={arrow}>{sub}</DropdownItem>;
                        })}


                    </div>
                </CSSTransition>
            )}

        </div>
    );
}

export default Filter;
